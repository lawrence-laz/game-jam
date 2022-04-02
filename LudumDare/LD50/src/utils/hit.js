import firstOrDefault from "./first-or-default.js";

const hit = (scene, source, x, y, radius) => {
    var bodies = scene.physics.overlapCirc(
        x, 
        y, 
        radius, 
        true, 
        true);
    var firstHittable = firstOrDefault(
        bodies,
        body => body.gameObject != source && body.gameObject.onHit);
    
    if (!firstHittable) {
        return;
    }
    firstHittable.gameObject.onHit();
};

export default hit;
