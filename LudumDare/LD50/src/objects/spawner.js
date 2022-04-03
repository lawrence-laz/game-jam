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

        scene.add.existing(this)

        scene.time.addEvent({
            delay: 1000,
            callback: () => this.trySpawnSomething(),
            loop: true
        });

        // this.spawnFlyingImp();
        this.spawnSpikes();

    }

    trySpawnSomething() {

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

        let targetX = Phaser.Math.Between(0, this.grid.width - 1);
        this.grid.setCellTo(object, targetX, 0);
        this.scene.objects.add(object);

    }

    spawnObjectFree(object) {

        this.scene.objects.add(object);

    }

}

export default Spawner;
