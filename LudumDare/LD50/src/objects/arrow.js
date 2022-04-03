import Hero from "./hero.js";

class Arrow extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'arrow') {
        super(scene, x, y, texture)

        this.health = 1;
        this.speed = 150;
        this.stickToObject;
        this.stickToOffset;

        this.setOrigin(0.8, 0.5);
        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.allowGravity = false;
        this.body.setSize(5, 5);
        this.body.offset.x = 11;
        scene.events.on('update', this.update, this)
        this.collider = this.scene.objects.add(this);
    }

    flyTo(fromX, fromY, toX, toY) {
        this.x = fromX;
        this.y = fromY;
        let currentPosition = new Phaser.Math.Vector2(fromX, fromY);
        let targetPosition = new Phaser.Math.Vector2(toX, toY);
        let direction = targetPosition.subtract(currentPosition).normalize();
        let velocity = direction.setLength(this.speed);
        this.body.setVelocityX(velocity.x);
        this.body.setVelocityY(velocity.y);
        this.rotation = Phaser.Math.Angle.Between(
            fromX, fromY,
            toX, toY);
    }

    stickTo(object) {
        this.stickToObject = object;
        this.stickToOffset = new Phaser.Math.Vector2(
            this.x - object.x,
            this.y - object.y
        );

        if (object.onHit) {
            object.onHit(this, 3);
        }

        this.collider.children.delete(this);

        this.scene.time.delayedCall(1000, () => this.destroy());
    }

    update(time, delta) {

        if (!this.active) {
            return;
        }

        if (this.stickToObject) {
            this.x = this.stickToObject.x + this.stickToOffset.x;
            this.y = this.stickToObject.y + this.stickToOffset.y;
        }
    }

    onHit(source) {

        this.setTintFill(0xffffff);

        this.scene?.cameras?.main?.shake(50, 0.01);
        this.scene.time.delayedCall(100, () => {
            this.clearTint();
            this.health -= 1;
            if (this.health <= 0) {
                this.destroy();
            }
        }, [], this);

    }
}

export default Arrow;
