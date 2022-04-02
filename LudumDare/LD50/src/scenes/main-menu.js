class MainMenu extends Phaser.Scene {

    constructor ()
    {
        super('main-menu');
        this.playButton;
    }

    preload ()
    {
    }

    create ()
    {
        this.playButton = this.add.image(400, 300, 'play-button');

        this.input.keyboard.addCapture('UP, DOWN, LEFT, RIGHT')

        this.input.keyboard.on('keydown-UP', function (event) {

            this.playButton.y -= 4;

        }, this);

        this.input.keyboard.on('keydown-DOWN', function (event) {

            this.playButton.y += 4;

        }, this);

        this.input.keyboard.on('keydown-LEFT', function (event) {

            console.log('A left');
            this.playButton.x -= 4;

        }, this);

        this.input.keyboard.on('keydown-RIGHT', function (event) {

            console.log('A right');
            this.playButton.x += 4;

        }, this);

        this.playButton.setInteractive();
        this.playButton.on('pointerdown', function () {

            console.log('down');
            // this.scene.pause();
            // this.scene.run('level');
            this.scene.transition({ target: 'level', duration: 200 });

        }, this);

    }

    update ()
    {
        this.playButton.rotation += 0.001;
    }

}

export default MainMenu;
