import Hero from "../objects/hero.js";
import Imp from "../objects/imp.js";
import firstOrDefault from "./first-or-default.js";

const hit = (scene, source, x, y, radius, damage = 1) => {
    var bodies = scene.physics
        .overlapCirc(x, y, radius, true, true)
        .sort((a, b) => Phaser.Math.Distance.Between(a.x, a.y, x, y) - Phaser.Math.Distance.Between(b.x, b.y, x, y));

    var firstHittable = firstOrDefault(
        bodies,
        body =>
            body.gameObject != source
            && body.gameObject.onHit
            && (!(source instanceof Hero) || body.gameObject.name != 'fence')
            && (!(source instanceof Imp) || !(body.gameObject instanceof Imp)));

    if (!firstHittable) {
        return;
    }
    firstHittable.gameObject.onHit(source, damage);
};

const swing = (scene, source, targetX, targetY, damage = 1) => {
    let woosh = scene.add.sprite(0, 0, 'woosh1');
    woosh.x = targetX;
    woosh.y = targetY;
    woosh.on('animationcomplete', function (anim, frame) {
        woosh.destroy();
    }, woosh);
    woosh.play('woosh');
    woosh.rotation = Phaser.Math.Angle.Between(
        source.x, source.y,
        targetX, targetY);
    woosh.flipY = Math.abs(woosh.rotation) > Phaser.Math.TAU;

    hit(scene, source, targetX, targetY, 3, damage);
}

const explode = (scene, source, targetX, targetY) => {

    if (!scene) {
        console.log('explode with undefined scene');
        return;
    }

    let explosion = scene.add.sprite(0, 0, 'explosion1');
    explosion.x = targetX;
    explosion.y = targetY;
    explosion.on('animationcomplete', function (anim, frame) {
        explosion.destroy();
    }, explosion);
    explosion.play('explode');

    hit(scene, source, targetX, targetY, 3);
    hit(scene, source, targetX, targetY, 3);
    hit(scene, source, targetX, targetY, 3);
}

export { hit, swing, explode };
