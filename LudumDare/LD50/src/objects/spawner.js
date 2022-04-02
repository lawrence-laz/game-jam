import Box from './box.js';
import Imp from './imp.js';

class Spawner extends Phaser.GameObjects.Sprite {

    constructor(grid, scene, x = 0, y = 0, texture = 'empty') {
        super(scene, x, y, texture)

        this.grid = grid;
        this.scene = scene;

        scene.add.existing(this)

        scene.time.addEvent({
            delay: 1000,
            callback: () => this.trySpawnSomething(),
            loop: true
        });
    }

    trySpawnSomething() {

        if (Phaser.Math.Between(0, 1) == 0) {
            return;
        }


        if (Phaser.Math.Between(0, 3) == 0) {
            this.spawnImp();
        } else {
            this.spawnBox();
        }

    }

    spawnImp() {
        let imp = new Imp(this.scene, 100, 0);
        this.spawnObject(imp);
    }

    spawnBox() {

        let box = new Box(this.scene, 100, 0);
        this.spawnObject(box);

    }

    spawnObject(object) {

        let targetX = Phaser.Math.Between(0, this.grid.width - 1);
        this.grid.setCellTo(object, targetX, 0);
        this.scene.objects.add(object);

    }

}

export default Spawner;
