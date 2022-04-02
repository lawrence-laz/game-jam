const helloWorld = (phaser, x, y) => {
    return phaser.add.text(x, y, 'Hello, world!', {
        fontFamily: 'font1',
    });
};

export default helloWorld;
