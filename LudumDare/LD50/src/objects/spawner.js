import firstOrDefault from '../utils/first-or-default.js';
import Arrow from './arrow.js';
import Bomb from './bomb.js';
import Box from './box.js';
import FlyingImp from './flying-imp.js';
import Imp from './imp.js';
import Spikes from './spikes.js';

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
        this.wave3ImpsWithSpikes = [
            null, null, this.spawnSpikes, this.spawnSpikes, this.spawnSpikes,
            null, null, this.spawnImp, this.spawnImp, this.spawnImp, this.spawnImp,
            () => this.genericSpawns.push(this.spawnBomb),
            () => this.genericSpawns.push(this.spawnBomb),
            () => this.genericSpawns.push(this.spawnSpikes),
            () => this.genericSpawns.push(this.spawnBox),
            () => this.genericSpawns.push(this.spawnBox),
            () => this.genericSpawns.push(this.spawnBox),
            () => this.genericSpawns.push(this.spawnBox),
        ];
        this.wave4FlyingImp = [
            null, null, this.spawnFlyingImp
        ];
        this.wave5GroundAndFlyingImps = [
            null, null, this.spawnImp, this.spawnImp, this.spawnImp,
            null, null, this.spawnFlyingImp
        ];
        this.wave6ChubbyImp = [
            null, null, this.spawnBomb, this.spawnBomb, this.spawnBomb,
            null, null, this.spawnChubbyImp
        ];
        this.wave7ArrowRain = [
            null, null, null,
            () => this.spawnArrows([
                [0 * 16 + 8, 0, 0 * 16 + 8, 1],
                [2 * 16 + 8, 0, 2 * 16 + 8, 1],
                [4 * 16 + 8, 0, 4 * 16 + 8, 1],
                [6 * 16 + 8, 0, 6 * 16 + 8, 1],
                [8 * 16 + 8, 0, 8 * 16 + 8, 1],
            ]),
            this.spawnBox,
            this.spawnFlyingImp, this.spawnFlyingImp
        ],
            this.wave8ChubbyParty = [
                null, null,
                this.spawnChubbyImp, this.spawnChubbyImp, this.spawnChubbyImp
            ],
            this.wave9ArrowRainingCatsAndDogs = [
                null, null, null,
                () => this.spawnArrows([
                    [0 * 16 + 8, 0, 0 * 16 + 8, 1],
                    [2 * 16 + 8, 0, 2 * 16 + 8, 1],
                    [4 * 16 + 8, 0, 4 * 16 + 8, 1],
                    [6 * 16 + 8, 0, 6 * 16 + 8, 1],
                    [8 * 16 + 8, 0, 8 * 16 + 8, 1],
                ]),
                () => this.spawnArrows([
                    [1 * 16 + 8, 0, 1 * 16 + 8, 1],
                    [3 * 16 + 8, 0, 3 * 16 + 8, 1],
                    [5 * 16 + 8, 0, 5 * 16 + 8, 1],
                    [7 * 16 + 8, 0, 7 * 16 + 8, 1],
                    [9 * 16 + 8, 0, 9 * 16 + 8, 1],
                ]),
                () => this.grid.setUpdateTickDelay(400),
            ],

            this.waves = [
                this.wave1JustImps,
                this.wave2ImpsWithBomb,
                this.wave3ImpsWithSpikes,
                this.wave4FlyingImp,
                this.wave5GroundAndFlyingImps,
                this.wave6ChubbyImp,
                this.wave7ArrowRain,
                this.wave8ChubbyParty,
                this.wave9ArrowRainingCatsAndDogs,
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

    }

    increaseGridSpeed() {
        this.grid
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

        if (!this.currentWave || this.currentWave.length == 0) {
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

            this.scene.sound.play('new-wave');
            let text = this.scene.add.bitmapText(
                5 * 16, 6 * 16, 'font', `Wave ${this.currentWaveIndex}`);
            text.setOrigin(0.5);
            text.setFontSize(12);
            text.setMaxWidth(10 * 16);
            this.scene.time.delayedCall(2000, () => text.destroy());
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
            this.scene.time.delayedCall(2000, () => text.destroy());
        }

        let impsCount = this.scene.children.list
            .filter(x => x instanceof FlyingImp || x instanceof Imp)
            .length;
        let flyingImpsCount = this.scene.children.list
            .filter(child => child instanceof FlyingImp)
            .length;

        if (impsCount <= 2) {

            let impSpawners = [this.spawnChubbyImp, this.spawnImp];
            if (flyingImpsCount == 0) {
                impSpawners.push(this.spawnFlyingImp);
            }

            for (var i = 0; i < 3; ++i) {

                let spawnIndex = Phaser.Math.Between(0, impSpawners.length - 1);
                impSpawners[spawnIndex].call(this);
                if (impSpawners[spawnIndex] == this.spawnFlyingImp) {
                    impSpawners.splice(spawnIndex, 1);
                }

            }
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

        if (!this.trySpawnWave() || (this.waves.length == 0 && this.currentWave.length == 0)) {
            this.trySpawnProcedural();
            this.trySpawnGeneric();
        }

        this.trySpawnGeneric();
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

    spawnArrows(coords) {

        for (var i = 0; i < coords.length; ++i) {
            this.spawnArrow(
                coords[i][0],
                coords[i][1],
                coords[i][2],
                coords[i][3]);
        }
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
