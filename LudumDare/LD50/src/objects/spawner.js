import firstOrDefault from '../utils/first-or-default.js';
import Arrow from './arrow.js';
import Bomb from './bomb.js';
import Box from './box.js';
import FlyingImp from './flying-imp.js';
import Imp from './imp.js';
import Spikes from './spikes.js';

// TODO: spawn waves/prefabs
// TODO: spawn/movement speed increase.

class Spawner extends Phaser.GameObjects.Sprite {

    constructor(grid, scene, x = 0, y = 0, texture = 'empty') {
        super(scene, x, y, texture)

        this.grid = grid;
        this.scene = scene;
        this.maxFlyingImpsAllowed = 1;

        this.currentWave = null;
        this.currentWaveIndex = 0;
        this.finalWaveLoaded = false;

        // ---------------------------------------------- Waves
        this.wave1JustImps = [
            null, null, this.spawnImp, this.spawnImp, this.spawnImp
        ];
        this.wave2ImpsWithBomb = [
            null, null, this.spawnBomb, null, null, this.spawnImp, this.spawnImp, this.spawnImp, this.spawnImp
        ];
        
        this.waves = [
            // this.wave1JustImps,
            this.wave2ImpsWithBomb,
        ];
        // ----------------------------------------------------

        this.genericSpawns = [
            this.spawnBox
        ];

        scene.add.existing(this)

        scene.time.addEvent({
            delay: 1000,
            callback: () => this.trySpawnSomething(),
            loop: true
        });

        // this.spawnFlyingImp();
        // this.spawnSpikes();

    }

    isWaveCleared() {

        if (!this.scene || !this.active) {
            return;
        }

        let anyEnemy = firstOrDefault(
            this.scene.children.list,
            x => x instanceof FlyingImp
                || x instanceof Imp);

        let isCleared = anyEnemy == null;

        return isCleared;
    }

    trySpawnWave() {

        if (!this.tryLoadNextWave()) {
            return false;
        }

        if (this.currentWave.length == 0) {
            // This wave is fully spawned, 
            // waiting for clearance to load next.
            return true;
        }

        let spawnNext = this.currentWave[0];
        this.currentWave.shift();
        if (spawnNext) {
            spawnNext.call(this);
        }

        return true;
    }

    tryLoadNextWave() {

        if (!this.currentWave || this.currentWave.length == 0) {

            if (!this.isWaveCleared()) {
                // Still wave in progress.
                return true;
            }

            if (this.waves.length == 0) {
                return false;
            }

            // New wave
            this.currentWave = this.waves[0];
            this.waves.shift();
            this.currentWaveIndex += 1;
            let text = this.scene.add.bitmapText(
                5 * 16, 6 * 16, 'font', `Wave ${this.currentWaveIndex}`);
            text.setOrigin(0.5);
            text.setFontSize(12);
            text.setMaxWidth(10 * 16);
            this.scene.time.delayedCall(1000, () => text.destroy());
        }

        return true;
    }

    trySpawnProcedural() {

        if (!this.finalWaveLoaded) {
            this.finalWaveLoaded = true;
            let text = this.scene.add.bitmapText(
                5 * 16, 6 * 16, 'font', `Final wave`);
            text.setOrigin(0.5);
            text.setFontSize(12);
            text.setMaxWidth(10 * 16);
            this.scene.time.delayedCall(1000, () => text.destroy());
        }

    }

    trySpawnGeneric() {

        if (Phaser.Math.Between(0, 1) == 0) {
            // Don't spawn this time.
            return;
        }

        let randomGeneric = this.genericSpawns[Phaser.Math.Between(0, this.genericSpawns.length - 1)];
        if (randomGeneric) {
            randomGeneric.call(this);
        }
    }

    trySpawnSomething() {

        if (!this.trySpawnWave()) {
            this.trySpawnProcedural();
        }

        this.trySpawnGeneric();
        return;

        if (Phaser.Math.Between(0, 1) == 0) {
            return;
        }

        let flyingImps = this.scene.children.list.filter(child => child instanceof FlyingImp)
        if (flyingImps.length > 0) {
            if (Phaser.Math.Between(0, 1) == 0) {
                this.spawnBox();
            }
        }

        let roll = Phaser.Math.Between(0, 3);
        if (roll == 0) {

            if (flyingImps.length < this.maxFlyingImpsAllowed
                && Phaser.Math.Between(0, 3) == 0) {
                this.spawnFlyingImp();
            } else {
                // this.spawnImp();
                this.spawnChubbyImp();
            }
        } else if (roll == 1) {
            if (Phaser.Math.Between(0, 1) == 0) {
                this.spawnSpikes();
            } else {
                this.spawnBomb();
            }
        } else {
            this.spawnBox();
        }

    }

    spawnBomb() {
        let bomb = new Bomb(this.scene, 100, 0);
        this.spawnObjectInGrid(bomb);
    }

    spawnSpikes() {
        let spikes = new Spikes(this.scene, 100, 0);
        this.spawnObjectInGrid(spikes);
    }

    spawnImp() {
        let imp = new Imp(this.grid, this.scene, 100, 0);
        this.spawnObjectInGrid(imp);
    }


    spawnChubbyImp() {
        let imp = new Imp(this.grid, this.scene, 100, 0);
        imp.play('chubby-imp-idle');
        imp.corpseTexture = 'chubby-imp-corpse';
        imp.isChubby = true;
        this.spawnObjectInGrid(imp);
    }

    spawnFlyingImp() {
        let targetX = Phaser.Math.Between(0, (this.grid.width - 1) * 16);
        let imp = new FlyingImp(this.grid, this.scene, targetX, 0);
        this.spawnObjectFree(imp);
    }

    spawnArrow(fromX, fromY, toX, toY) {
        let arrow = new Arrow(this.scene, fromX, fromY);
        this.spawnObjectFree(arrow);
        arrow.flyTo(fromX, fromY, toX, toY);

        return arrow;
    }

    spawnBox() {

        let box = new Box(this.scene, 100, 0);
        this.spawnObjectInGrid(box);

    }

    spawnObjectInGrid(object) {

        let targetX = this.getRandomGridCellX();
        if (targetX == null) {
            return;
        }
        this.grid.setCellTo(object, targetX, 0);
        this.scene.objects.add(object);

    }

    getRandomGridCellX() {
        let attempts = 100;
        do {
            let x = Phaser.Math.Between(0, this.grid.width - 1);
            let occupied = this.grid.cells[x][0] != null;
            if (!occupied) {
                return x;
            }

        } while (attempts-- > 0);

        return null;
    }

    spawnObjectFree(object) {

        this.scene.objects.add(object);

    }

}

export default Spawner;
