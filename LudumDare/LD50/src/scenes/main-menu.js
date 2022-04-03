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



        this.playButton.setInteractive();
        this.playButton.on('pointerdown', function () {

            this.scene.transition({ target: 'level', duration: 200 });

        }, this);

    }

    update ()
    {
        this.playButton.rotation += 0.001;
    }

}

export default MainMenu;
