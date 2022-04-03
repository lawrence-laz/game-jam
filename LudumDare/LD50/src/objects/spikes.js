import Hero from "./hero.js";

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
        this.body.setSize(14, 10);
        this.body.offset.x = 1;
        this.body.offset.y = 6;
        scene.events.on('update', this.update, this)
        this.open = false;

        scene.time.addEvent({
            delay: 2000,
            callback: () => this.updateTick(),
            loop: true
        });

    }

    update(time, delta) {

    }

    updateTick() {

        if (!this.active) {
            return;
        }

        let willBeOpen = !this.open
        if (willBeOpen) {
            this.scene.time.delayedCall(500, () => this.open = willBeOpen, [], this);
        } else {
            this.open = willBeOpen;
        }
        this.setTexture(willBeOpen ? 'spikes4' : 'spikes1');
        this.play(willBeOpen ? 'spikes-open' : 'spikes-close');

    }

    onHit(source, damage = 1) {
        this.setTintFill(0xffffff);


        this.scene?.cameras?.main?.shake(50, 0.01);
        this.scene.time.delayedCall(100, () => {
            this.clearTint();
            this.health -= damage;
            if (this.health <= 0) {
                this.destroy();
            }
        }, [], this);
    }
}

export default Spikes;
