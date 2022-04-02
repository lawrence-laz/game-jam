import { swing } from "../utils/hit.js";

class Hero extends Phaser.GameObjects.Sprite {

    constructor(grid, scene, x = 16 * 5 + 4, y = 16 * 10, texture = 'hero') {
        super(scene, x, y, texture)

        this.speed = 100;
        this.jumpSpeed = -100;
        this.jumpCount = 2;
        this.isTryingToJump = false;
        this.setName('hero');
        this.grid = grid;
        this.alignTween = false;
        this.setOrigin(0, 0);
        this.health = 3;

        this.cursor = scene.input.keyboard.createCursorKeys();

        this.target = scene.add.sprite(100, 50, 'target');

        scene.add.existing(this)
        scene.physics.add.existing(this)
        this.body.setCollideWorldBounds(true);
        this.body.setSize(14, 14);
        scene.events.on('update', this.update, this)

        this.woosh = scene.add.sprite(0, 0, 'woosh1');
        this.woosh.visible = false;

        scene.input.on('pointerdown', function (pointer) {
            this.swingSword();
        }, this);
    }

    swingSword() {
        swing(this.scene, this, this.target.x, this.target.y);
    }

    update(time, delta) {

        if (!this.active) {
            return;
        }

        this.handleInput(time);
        this.updateTargetPosition();
        // this.alignWithGrid();
        this.updateGridCell();
    }

    updateGridCell() {
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
                debugger; // WHAT?
            }
        }
    }

    alignWithGrid() {
        if (this.body.velocity.length() == 0) {
            let alignedPosition = this.grid.getAlignedPosition(this.x, this.y);

            if (this.alignTween
                || (alignedPosition.x == this.x && alignedPosition.y == this.y)) {
                return;
            }
            // debugger;

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

        if (this.cursor.left.isDown) {
            this.body.setVelocityX(-this.speed);
        } else if (this.cursor.right.isDown) {
            this.body.setVelocityX(this.speed);
        } else {
            this.body.setVelocityX(0);
        }

        if (Phaser.Input.Keyboard.JustDown(this.cursor.up)
            && this.jumpCount > 0) {
            this.isTryingToJump = true;
            this.jumpCount -= 1;
        } else if (this.body.touching.down) {
            this.jumpCount = 2;
        }

        if (Phaser.Input.Keyboard.JustUp(this.cursor.up)) {
            this.isTryingToJump = false;
        }

        if (this.isTryingToJump && this.cursor.up.isDown) {

            let duration = time - this.cursor.up.timeDown;
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
        this.target.x = this.scene.input.activePointer.worldX;
        this.target.y = this.scene.input.activePointer.worldY;
        let radius = 20;
        var distance = Phaser.Math.Distance.Between(
            this.x, this.y, this.target.x, this.target.y);
        if (distance > radius) {
            var scale = distance / radius;
            this.target.x = this.x + (this.target.x - this.x) / scale;
            this.target.y = this.y + (this.target.y - this.y) / scale;
        }

    }

    onDestroy() {
        this.scene.scene.restart();
    }

    onHit() {
        this.health -=1;
        if (this.health <= 0) {
            alert("You've been killed!");
            this.onDestroy();
        }
    }
}

export default Hero;
