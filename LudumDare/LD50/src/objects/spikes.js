class Spikes extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'spikes') {
        super(scene, x, y, texture)

        this.health = 3;

        this.setOrigin(0, 0);
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
        this.health -=1;
        if (this.health <= 0) {
            this.destroy();
        }
    }
}

export default Spikes;
