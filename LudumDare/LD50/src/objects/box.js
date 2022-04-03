import Hero from "./hero.js";

class Box extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'box') {
        super(scene, x, y, texture)

        this.health = 3;
        this.maxHealth = 3;

        this.setOrigin(0, 0);
        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.setCollideWorldBounds(true);
        this.body.setImmovable(true);
        this.body.allowGravity = false;
        this.damagedTexture = null;
        scene.events.on('update', this.update, this)

    }

    update(time, delta) {

        if (!this.active) {
            return;
        }

        if (this.damagedTexture 
            && this.texture != this.damagedTexture
            && this.health / this.maxHealth <= 0.5) {
            this.setTexture(this.damagedTexture);
        }

    }

    onHit(source, damage = 1) {

        this.setTintFill(0xffffff);

        this.scene?.cameras?.main?.shake(50, 0.01);
        this.scene.time.delayedCall(100, () => {
            this.clearTint();
            this.health -= damage;
            if (this.health <= 0) {
                this.scene?.sound.play('final-hit');
                this.destroy();
            }
        }, [], this);

    }
}

export default Box;
