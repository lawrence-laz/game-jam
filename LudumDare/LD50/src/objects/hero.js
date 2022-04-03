import { swing } from "../utils/hit.js";
import Imp from "./imp.js";

var hero;

class Hero extends Phaser.GameObjects.Sprite {

    constructor(grid, scene, x = 16 * 5 + 4, y = 16 * 10, texture = 'hero1') {
        super(scene, x, y, texture)

        hero = this;
        this.speed = 100;
        this.jumpSpeed = -100;
        this.jumpCount = 2;
        this.isTryingToJump = false;
        this.setName('hero');
        this.grid = grid;
        this.alignTween = false;
        this.setOrigin(0, 0);
        this.health = 3 * 3;
        this.play('hero-idle');

        this.lastSwingAt = 0;
        this.swingPeriod = 200;
        this.lastHitOn = 0;
        this.restoreHealthPeriod = 4000;

        this.cursor = scene.input.keyboard.createCursorKeys();
        this.w = scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.W);
        this.a = scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.A);
        this.s = scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.S);
        this.d = scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.D);

        this.target = scene.add.sprite(100, 50, 'target');
        this.corpseTexture = 'hero-corpse';

        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.setCollideWorldBounds(true);
        this.body.setSize(11, 14);
        this.body.gravity = new Phaser.Math.Vector2(0, 100);
        scene.events.on('update', this.update, this)

        this.woosh = scene.add.sprite(0, 0, 'woosh1');
        this.woosh.visible = false;

        scene.time.addEvent({
            delay: this.restoreHealthPeriod,
            callback: () => this.restoreHealth(),
            loop: true
        });

        scene.time.addEvent({
            delay: 100,
            callback: () => this.updateTick(),
            loop: true
        });

        scene.input.on('pointerdown', function (pointer) {
            this.swingSword();
        }, this);

        var particles = this.scene.add.particles('dust1');
        this.runDustEmitter = particles.createEmitter({
            callbackScope: this,
            speed: {
                onEmit: function (particle, key, t, value) {
                    return hero.body?.speed / 50;
                }
            },
            lifespan: {
                onEmit: function (particle, key, t, value) {
                    if (hero.body?.touching.down) {
                        return Phaser.Math.Percent(hero.body.speed, 50, 100) * 200;
                    } else {
                        return 0;
                    }
                }
            },
            alpha: {
                onEmit: function (particle, key, t, value) {
                    if (hero.body?.touching.down && hero.body.speed != 0) {
                        return Phaser.Math.Between(0.5, 1);//return Phaser.Math.Percent(hero.body.speed, 0, 300) * 1000;
                    } else {
                        return 0;
                    }
                }
            },
            rotate: { min: 0, max: 360 },
            scale: { start: 1.2, end: 0.5 },
            gravityY: 1,
            blendMode: 'ADD',
            emitZone: {
                source: {
                    getRandomPoint: (point) => {
                        point.x = Phaser.Math.Between(-2, 2);
                        point.y = Phaser.Math.Between(-1, 1);
                    },
                },
                type: 'random'
            }
        });

        this.impactDustEmitter = particles.createEmitter({
            callbackScope: this,
            x: -100,
            y: -100,
            speed: {
                onEmit: function (particle, key, t, value) {
                    return 1;
                }
            },
            lifespan: {
                onEmit: function (particle, key, t, value) {
                    return Phaser.Math.Between(100, 200);
                }
            },
            alpha: {
                onEmit: function (particle, key, t, value) {
                    return Phaser.Math.Between(0.5, 1);//return Phaser.Math.Percent(hero.body.speed, 0, 300) * 1000;
                }
            },
            rotate: { min: 0, max: 360 },
            scale: { start: 1.2, end: 1 },
            gravityY: 10,
            blendMode: 'ADD',
            emitZone: {
                source: {
                    getRandomPoint: (point) => {
                        point.x = Phaser.Math.Between(-2, 2);
                        point.y = Phaser.Math.Between(-1, 1);
                    },
                },
                type: 'random'
            }
        });

        this.runDustEmitter.startFollow(this, 8, 14);
    }

    setHealthTint() {
        this.clearTint();

        if (this.health <= 2 * 3) {
            this.setTint(0xff4444);
        }
        if (this.health <= 1 * 3) {
            this.setTint(0xff0000);
        }
    }

    restoreHealth() {

        if (this.gameOver) {
            return;
        }

        if (!this.active) {
            return;
        }

        if (this.scene.time.now - this.lastHitOn < this.restoreHealthPeriod) {
            return;
        }

        if (this.health < 3 * 3) {
            this.health += 1 * 3;
            this.setHealthTint();
        }
    }

    swingSword() {

        if (this.gameOver) {
            return;
        }

        if (!this.active) {
            return;
        }

        if (this.scene.time.now - this.lastSwingAt < this.swingPeriod) {
            return;
        }

        if (this.anims?.currentAnim?.key == 'hero-swing') {
            return;
        }

        this.lastSwingAt = this.currentTime;

        this.play('hero-swing').chain({
            key: 'hero-idle',
            repeat: -1
        });

        swing(this.scene, this, this.target.x, this.target.y);
    }

    updateTick() {

        if (this.gameOver) {
            return;
        }

        if (!this.active) {
            return;
        }

        this.updateGridCell();
    }

    update(time, delta) {

        if (this.gameOver) {
            return;
        }

        if (!this.active) {
            return;
        }

        this.handleInput(time);
        this.updateTargetPosition();
        // this.alignWithGrid();

    }

    updateGridCell() {

        if (this.gameOver) {
            return;
        }

        for (var x = 0; x < this.grid.cells.length; ++x)
            for (var y = 0; y < this.grid.cells[0].length; ++y) {
                if (this.grid.cells[x][y] instanceof Hero) {
                    this.grid.cells[x][y] = null;
                }
            }

        let alignedPosition = this.grid.getAlignedPosition(this.x, this.y);
        let heroCell = this.grid.getCellForPosition(
            alignedPosition.x,
            alignedPosition.y);

        if (this.grid.cells[heroCell.x][heroCell.y] == null) {
            this.grid.cells[heroCell.x][heroCell.y] = this;
        } else {
            if (this.grid.cells[heroCell.x][heroCell.y + 1] == null) {
                this.grid.cells[heroCell.x][heroCell.y + 1] = this;
                this.body.setAccelerationY(Math.abs(this.body.acceleration.y));
            } else {
                // TODO: ??
                // debugger; // WHAT?
            }
        }
    }

    alignWithGrid() {

        if (this.gameOver) {
            return;
        }

        if (this.body.velocity.length() == 0) {
            let alignedPosition = this.grid.getAlignedPosition(this.x, this.y);

            if (this.alignTween
                || (alignedPosition.x == this.x && alignedPosition.y == this.y)) {
                return;
            }

            this.alignTween = this.scene.tweens.add({
                targets: this,
                x: alignedPosition.x,
                y: alignedPosition.y + 2,
                duration: 300,
                // ease: 'Power2',
                // paused: true
            });
        } else {
            if (this.alignTween) {
                this.alignTween.stop();
                this.alignTween = null;
            }
        }
    }
    handleInput(time) {

        if (this.gameOver) {
            return;
        }

        if (this.cursor.left.isDown || this.a.isDown) {
            this.body.setVelocityX(-this.speed);
        } else if (this.cursor.right.isDown || this.d.isDown) {
            this.body.setVelocityX(this.speed);
        } else {
            this.body.setVelocityX(0);
        }

        if ((Phaser.Input.Keyboard.JustDown(this.cursor.up) || Phaser.Input.Keyboard.JustDown(this.w))
            && this.jumpCount > 0) {
            this.isTryingToJump = true;
            this.jumpCount -= 1;
            this.impactDustEmitter.explode(30, this.x + 8, this.y + 14);
        } else if (this.body.touching.down || this.body.touching.left || this.body.touching.right) {
            this.jumpCount = 2;
        }

        if (Phaser.Input.Keyboard.JustUp(this.cursor.up) || Phaser.Input.Keyboard.JustUp(this.w)) {
            this.isTryingToJump = false;
        }

        if (this.isTryingToJump && (this.cursor.up.isDown || this.w.isDown)) {

            let duration = time - Math.max(this.cursor.up.timeDown, this.w.timeDown);
            if (duration < 100) {
                this.body.setVelocityY(this.jumpSpeed);
            }
        }

        // this.input.on('pointerdown', function (pointer) {
        //     console.log('down');

        //     // var text = helloWorld(this, pointer.x, pointer.y);
        // }, this);


    }

    updateTargetPosition() {

        if (this.gameOver) {
            return;
        }

        this.target.x = this.scene.input.activePointer.worldX;
        this.target.y = this.scene.input.activePointer.worldY;
        let pivotX = this.x + 8;
        let pivotY = this.y + 8;
        let radius = 20;
        var distance = Phaser.Math.Distance.Between(
            pivotX, pivotY, this.target.x, this.target.y);
        if (distance > radius) {
            var scale = distance / radius;
            this.target.x = pivotX + (this.target.x - pivotX) / scale;
            this.target.y = pivotY + (this.target.y - pivotY) / scale;
        }

        if (this.target.x - (this.x + 8) > 0) {
            this.flipX = true;
        } else {
            this.flipX = false;
        }

    }

    onDestroy() {
        // TODO: slow down time and show game over after few moments.
        this.scene.scene.restart();
    }

    doGameOver() {

        if (this.gameOver) {
            return;
        }

        this.gameOver = true;
        this.body.setVelocityX(0);
        this.body.setVelocityY(0);
    }

    convertToCorpse() {

        if (!this.scene) {
            return;
        }

        let corpse = this.scene.add.sprite(this.x, this.y, this.corpseTexture);
        corpse.setOrigin(0, 0);
        this.setVisible(false);
    }

    onHit(source, damage = 1) {

        this.setTintFill(0xff0000);


        this.scene?.cameras?.main?.shake(100);
        this.scene.time.delayedCall(100, () => {
            this.health -= damage;
            this.setHealthTint();
            this.lastHitOn = this.scene.time.now;
            if (this.health <= 0) {
                this.convertToCorpse();
                this.scene.gameOver("You've been slain!", this);
            }
        }, [], this);

    }
}

export default Hero;
