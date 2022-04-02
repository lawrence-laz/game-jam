import { explode } from "../utils/hit.js";

class Bomb extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'bomb1') {
        super(scene, x, y, texture)

        this.health = 20;
        this.triggered = false;
        this.setOrigin(0, 0);
        this.scene = scene;
        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.setCollideWorldBounds(true);
        this.body.setImmovable(true);
        this.body.allowGravity = false;
        scene.events.on('update', this.update, this)

    }

    update(time, delta) {

    }

    onHit() {
        if (this.triggered) {
            return;
        }

        this.triggered = true;

        // debugger;

        this.play('bomb-trigger');

        this.scene.time.addEvent({
            delay: 1500,
            callback: () => {

                let explosionArea = [
                    new Phaser.Math.Vector2(this.x + 0, this.y + 0),
                    new Phaser.Math.Vector2(this.x + 16, this.y + 0),
                    new Phaser.Math.Vector2(this.x + 16, this.y - 16),
                    new Phaser.Math.Vector2(this.x + 0, this.y - 16),
                    new Phaser.Math.Vector2(this.x - 16, this.y - 16),
                    new Phaser.Math.Vector2(this.x - 16, this.y + 0),
                    new Phaser.Math.Vector2(this.x - 16, this.y + 16),
                    new Phaser.Math.Vector2(this.x + 0, this.y + 16),
                    new Phaser.Math.Vector2(this.x + 16, this.y + 16),
                ];
                for (var i = 0; i < explosionArea.length; ++i) {
                    let explosionPoint = explosionArea[i];
                    explode(this.scene, this, explosionPoint.x, explosionPoint.y);
                }

                this.destroy();
            }
        })
    }
}

export default Bomb;
