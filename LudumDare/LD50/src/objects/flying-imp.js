import { swing } from "../utils/hit.js";
import Arrow from "./arrow.js";
import Hero from "./hero.js";
import Imp from "./imp.js";

class FlyingImp extends Phaser.GameObjects.Sprite {

    constructor(grid, scene, x = 0, y = 0, texture = 'flying-imp1') {
        super(scene, x, y, texture)
        this.scene = scene;
        this.grid = grid;
        this.setOrigin(0, 0);
        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.setCollideWorldBounds(true);
        this.body.setImmovable(true);
        this.body.allowGravity = false;
        this.body.setSize(15, 15);
        scene.events.on('update', this.update, this)
        this.health = 3;
        this.isStable = false;
        this.path = null;
        this.play('flying-imp-idle');
        this.corpseTexture = 'flying-imp-corpse';

        var tween = this.scene.tweens.add({
            targets: this,
            x: this.x,
            y: 32,
            duration: 2000,
        });

        scene.time.addEvent({
            delay: 3000,
            callback: () => this.updateTick(),
            loop: true
        });

    }

    updateTick() {

        if (!this.active) {
            return;
        }

        let hero = this.scene.children.getByName('hero');
        if (hero == null) {
            return;
        }

        if (this.tryAttack(hero)) {
            return;
        }
    }

    tryAttack(hero) {
        let heroPosition = new Phaser.Math.Vector2(hero.x + 8, hero.y + 8);
        let impPosition = new Phaser.Math.Vector2(this.x, this.y);

        this.scene.sound.play('arrow-shot');
        let arrow = new Arrow(this.scene, 0, 0);
        arrow.flyTo(
            impPosition.x + 8, impPosition.y + 16,
            heroPosition.x, heroPosition.y
        );

        return true;
    }

    update(time, delta) {

        if (!this.active) {
            return;
        }

        this.lookAtHero();

    }

    lookAtHero() {

        let hero = this.scene.children.getByName('hero');
        if (hero == null) {
            return;
        }

        this.flipX = hero.x < this.x;
    }

    onHit(source) {

        this.setTintFill(0xffffff);

        this.scene.sound.play('hit');
        this.scene?.cameras?.main?.shake(50, 0.01);
        this.scene.time.delayedCall(100, () => {
            this.clearTint();
            this.health -= 1;
            if (this.health <= 0) {
                if (this.scene) {
                    this.scene.sound.play(Phaser.Math.Between(0, 1) == 0
                        ? 'enemy-voice1'
                        : 'enemy-voice2');
                    let corpse = this.scene.add.sprite(this.x, this.y, this.corpseTexture);
                    corpse.setOrigin(0, 0);
                    this.scene.tweens.add({
                        targets: corpse,
                        x: this.x,
                        y: 15 * 16,
                        duration: 2000,
                    });
                }

                this.destroy();
            }
        }, [], this);
    }
}

export default FlyingImp;
