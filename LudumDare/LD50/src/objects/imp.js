import { swing } from "../utils/hit.js";

class Imp extends Phaser.GameObjects.Sprite {

    constructor(grid, scene, x = 0, y = 0, texture = 'imp') {
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

        scene.time.addEvent({
            delay: 1000,
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

        this.tryGoBeyondFence();
    }

    tryGoBeyondFence() {


        if (!this.isStable) {
            return;
        }

        let impCell = this.grid.getCellForPosition(this.x, this.y);
        // debugger;
        if (this.path == null) {
            this.path = this.grid.findPathDijkstra(impCell, new Phaser.Math.Vector2(4, this.grid.height));
            let currentPosition = this.path.pop();
        }
        if (this.path == null) {
            console.log("oh well");
        } else {
            console.log("heading for the gates!");

            if (this.path.length == 0) {
                this.path = null;
                return;
            }

            let nextCell = this.path.pop();
            let nextCellObject = this.grid.cells[nextCell.x][nextCell.y];

            if (nextCellObject == null) {
                this.grid.tryMoveTo(this, impCell.x, impCell.y, nextCell.x, nextCell.y);
            } else if (!(nextCell instanceof Imp)) {
                let nextCellPosition = this.grid.getPositionForCell(nextCell.x, nextCell.y);
                swing(this.scene, this, nextCellPosition.x, nextCellPosition.y);
            } else {
                // Friend imp is occupying my space.
            }
        }
    }

    tryAttack(hero) {
        let heroPosition = new Phaser.Math.Vector2(hero.x, hero.y);
        let impPosition = new Phaser.Math.Vector2(this.x, this.y);
        if (impPosition.distance(heroPosition) < 16 * 1.35) {
            let wooshPosition = impPosition.clone().add(heroPosition.clone().subtract(impPosition).normalize().setLength(8));
            swing(this.scene, this, wooshPosition.x, wooshPosition.y);

            return true;
        }

        return false;
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

    onHit() {
        this.health -= 1;
        if (this.health <= 0) {
            this.destroy();
        }
    }
}

export default Imp;
