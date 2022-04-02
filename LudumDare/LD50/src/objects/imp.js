class Imp extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'imp') {
        super(scene, x, y, texture)
        this.scene = scene;
        this.setOrigin(0, 0);
        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.setCollideWorldBounds(true);
        this.body.setImmovable(true);
        this.body.allowGravity = false;
        this.body.setSize(15, 15);
        scene.events.on('update', this.update, this)
        this.health = 3;

    }

    update(time, delta) {

        if (!this.active) {
            return;
        }

        this.lookAtHero();

    }

    lookAtHero() {

        let hero = this.scene.children.getByName('hero');
        this.flipX = hero.x < this.x;
    }

    onHit() {
        this.health -=1;
        if (this.health <= 0) {
            this.destroy();
        }
    }
}

export default Imp;
