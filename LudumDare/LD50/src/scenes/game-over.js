class GameOver extends Phaser.Scene {

    constructor() {
        super('game-over');
        this.playButton;
    }

    preload() {
    }

    create(data) {

        this.deathReason(data.reason);
        this.score(data.score);

        let text = this.add.bitmapText(
            5 * 16, 10 * 16, 'font', `Press to restart`);

        text.setOrigin(0.5);
        text.setFontSize(8);

        this.sound.play('game-over');

        this.allowRestart = false;
        this.restarting = false;
        this.shouldRestart = false;

        this.time.delayedCall(300, () => {
            this.allowRestart = true;
        });

        this.input.on('pointerdown', function (pointer) {
            if (!this.allowRestart) {
                return;
            }
            this.shouldRestart = true;
        }, this);

        this.input.keyboard.on('keydown', function (event) {
            if (!this.allowRestart) {
                return;
            }
            this.restartLevelScene();
        }, this);

    }

    restartLevelScene() {
        if (this.restarting) {
            return;
        }
        this.restart = true;

        this.scene.start('level');
    }

    update() {
        if (this.shouldRestart) {
            this.restartLevelScene();
        }
    }

    deathReason(reason) {

        let text = this.add.bitmapText(
            5 * 16, 6 * 16, 'font', reason);

        text.setOrigin(0.5);
        text.setFontSize(8);
        text.setMaxWidth(10 * 16);
    }

    score(number) {
        let text = this.add.bitmapText(
            5 * 16, 8 * 16, 'font', `Wave ${number}`);

        text.setOrigin(0.5);
        text.setFontSize(8);
    }

}

export default GameOver;
