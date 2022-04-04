class MainMenu extends Phaser.Scene {

    constructor() {
        super('main-menu');
        this.mainMenuSprite;
    }

    preload() {
    }

    create() {

        let music = this.sound.add('music');
        music.play({
            loop: true,
            volume: 0.4
        });

        this.mainMenuSprite = this.add.image(0, 0, 'main-menu');
        this.mainMenuSprite.setOrigin(0, 0);

        let title = this.add.bitmapText(
            5 * 16, 3 * 16, 'font', `Gate\nKeeper`);
        title.setOrigin(0.5);
        title.setFontSize(20);

        let startText = this.add.bitmapText(
            5 * 16, 6 * 16, 'font', `Press any key to start . . .`);
        startText.setOrigin(0.5);
        startText.setFontSize(8);
        startText.setMaxWidth(8 * 16);

        this.input.on('pointerdown', function (pointer) {
            this.shouldStart = true;
        }, this);

        this.input.keyboard.on('keydown', function (event) {
            this.startLevelScene();
        }, this);

    }

    startLevelScene() {
        if (this.restarting) {
            return;
        }
        this.restart = true;

        this.scene.start('level');
    }

    update() {
        if (this.shouldStart) {
            this.startLevelScene();
        }
    }

}

export default MainMenu;
