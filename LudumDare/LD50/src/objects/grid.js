import Box from "./box.js";
import Hero from "./hero.js";

class Grid extends Phaser.GameObjects.Sprite {

    constructor(scene, x = 0, y = 0, texture = 'grid') {
        super(scene, x, y, texture)
        this.scene = scene;
        this.setOrigin(0, 0);
        this.cells = [
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
            [null, null, null, null, null, null, null, null, null, null, null, null],
        ];

        this.cellSize = 16;
        this.width = 10;
        this.height = 12;

        scene.add.existing(this)
        scene.events.on('update', this.update, this)

        scene.time.addEvent({
            delay: 1000,
            callback: () => this.updateTick(),
            loop: true
        });

        this.createGates();

    }

    createGates() {
        for (var i = 0; i < this.cells.length; ++i) {
            let texture = i == 4
                ? 'left-gate'
                : i == 5
                ? 'right-gate'
                : 'fence';
            let object = new Box(this.scene, 0, 0, texture);
            this.setCellTo(object, i, this.height - 1);
            this.scene.objects.add(object);
        }
    }

    update(time, delta) {

    }

    updateTick() {

        // debugger;

        for (var x = 0; x < this.cells.length; ++x)
            for (var y = this.cells[0].length - 1; y >= 0; --y) {

                let cellObject = this.cells[x][y];
                if (cellObject == null || cellObject instanceof Hero) {
                    continue;
                } else if (cellObject.active === false) {
                    this.cells[x][y] = null;
                    continue;
                }

                this.tryMoveTo(cellObject, x, y, x, y + 1);

            }

    }

    setCellTo(object, x, y) {
        this.cells[x][y] = object;
        let position = this.getPositionForCell(x, y);
        object.x = position.x;
        object.y = position.y;
    }

    tryMoveTo(movingObject, beforeX, beforeY, afterX, afterY) {

        if (afterX < 0 || afterX >= this.cells.length
            || afterY < 0 || afterY >= this.cells[0].length) {
            return;
        }

        let targetObject = this.cells[afterX][afterY];
        if (targetObject != null)
        {
            if (!this.trySquish(movingObject, afterX, afterY)) {
                return;
            }
        }

        if (beforeX >= 0 && beforeX < this.cells.length
            && beforeY >= 0 && beforeY < this.cells[0].length) {
            this.cells[beforeX][beforeY] = null;
        }
        this.cells[afterX][afterY] = movingObject;

        let afterPosition = this.getPositionForCell(afterX, afterY);
        var tween = this.scene.tweens.add({
            targets: movingObject,
            x: afterPosition.x,
            y: afterPosition.y,
            ease: 'Power2',
            // paused: true
        });

    }

    trySquish(source, x, y) {
        // TODO
        return false;
    }

    tryMoveOffset(object, offsetX) {

        let beforeCell = this.getCellForPosition(object.x, object.y);
        let objectInGrid = this.cells[beforeCell.x][beforeCell.y];
        if (objectInGrid != object) {
            beforeCell.y += 1;
            objectInGrid = this.cells[beforeCell.x][beforeCell.y];
            if (objectInGrid != object) {
                return;
            }
        }

        let targetCell = this.getCellForPosition(object.x, object.y);
        targetCell.x += offsetX;
        let targetPosition = this.getPositionForCell(targetCell.x, targetCell.y);
        let threshold = 3; // Moving this amount up is acceptable.
        if (object.y > targetPosition.y + threshold) {
            // Avoid moving up.
            targetCell.y += 1;
        }

        this.tryMoveTo(
            object,
            beforeCell.x, beforeCell.y,
            targetCell.x, targetCell.y);
    }

    getCellForPosition(x, y) {
        let cell = new Phaser.Math.Vector2(
            Phaser.Math.FloorTo(x / this.cellSize),
            Phaser.Math.FloorTo(y / this.cellSize));

        return cell;
    }

    getPositionForCell(x, y) {
        let position = new Phaser.Math.Vector2(
            x * this.cellSize,
            y * this.cellSize);

        return position;
    }

    getAlignedPosition(x, y) {
        let cell = new Phaser.Math.Vector2(
            Phaser.Math.RoundTo(x / this.cellSize),
            Phaser.Math.RoundTo(y / this.cellSize));
        let alignedPosition = this.getPositionForCell(cell.x, cell.y);

        return alignedPosition;
    }
}

export default Grid;
