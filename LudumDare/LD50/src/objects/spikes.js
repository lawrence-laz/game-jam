class Spikes extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'spikes1') {
        super(scene, x, y, texture)

        this.health = 3;

        this.setOrigin(0, 0);
        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.setCollideWorldBounds(true);
        this.body.setImmovable(true);
        this.body.allowGravity = false;
        scene.events.on('update', this.update, this)
        this.open = false;

        scene.time.addEvent({
            delay: 2000,
            callback: () => this.updateTick(),
            loop: true
        });

        // this.on('animationcomplete', function (anim, frame) {
        //     this.open = !this.open;
        //     this.setTexture(this.open ? 'spikes4' : 'spikes1');
        //     this.play(this.open ? 'spikes-open' : 'spikes-close');
        // }, this);

    }

    update(time, delta) {

    }

    updateTick() {

        if (!this.active) {
            return;
        }

        this.open = !this.open;
        this.setTexture(this.open ? 'spikes4' : 'spikes1');
        this.play(this.open ? 'spikes-open' : 'spikes-close');

        // if (open) {
        //     this.play("spikes-close");
        // } else {
        //     this.play("spikes-open");
        // }
    }

    onHit() {
        this.health -= 1;
        if (this.health <= 0) {
            this.destroy();
        }
    }
}

export default Spikes;
