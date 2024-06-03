const fieldsFromModel = fetchFields();
async function displaySavedRectangleData() {
    const rectangleDataBody = document.getElementById("rectangleData");
    rectangleDataBody.innerHTML = "";
    const fieldOptions = {};
    fieldsFromModel.forEach((field) => {
        fieldOptions[field.id] = field.name;
    });

    for (let index = 0; index < rectangles.length; index++) {
        const rectangle = rectangles[index];
        const row = document.createElement("tr");

        console.log(rectangle.fieldId);
       // const fieldName = rectangle.fieldId || 'Unknown Field';
        const fieldName = fieldOptions[rectangle.fieldId] || 'Unknown Field';
        const cellContent = rectangle.isAnchorPoint ? 'Anchor Point' : fieldName;

        row.innerHTML = `
                <td>${rectangle.x}</td>
                <td>${rectangle.y}</td>
                <td>${rectangle.actualWidth}</td>
                <td>${rectangle.actualHeight}</td>
                <td>${cellContent}</td>
             
            `;
        rectangleDataBody.appendChild(row);
    }

    for (let i = 0; i < rectangles.length; i++) {
        const rectangle = rectangles[i];
        console.log(rectangle);
        const rect = new fabric.Rect({
            left: rectangle.x,
            top: rectangle.y,
            width: rectangle.actualWidth,
            height: rectangle.actualHeight,
            fill: "transparent",
            stroke: rectangle.isAnchorPoint ? "red" : "blue",
            strokeWidth: 2,
            borderColor: rectangle.isAnchorPoint ? "red" : "blue",
            cornerColor: rectangle.isAnchorPoint ? "red" : "blue",
            cornerSize: 10,
            selectable: true,
        });
        console.log(rectangle);
        rect.fieldId = rectangle.fieldId;
        console.log(rect.fieldId);
        canvas.add(rect);
    }


}
async function displayRectangleData() {
    const rectangleDataBody = document.getElementById("rectangleData");
    rectangleDataBody.innerHTML = "";

    const fieldOptions = {};
    fieldsFromModel.forEach((field) => {
        fieldOptions[field.id] = field.name;
    });

    for (let index = 0; index < rectangles.length; index++) {
        const rectangle = rectangles[index];
        const row = document.createElement("tr");
        console.log(rectangle);
        const fieldName = fieldOptions[rectangle.FieldId] || 'Unknown Field';
        const cellContent = rectangle.IsAnchorPoint ? 'Anchor Point' : fieldName;
        row.innerHTML = `
            <td>${rectangle.X}</td>
            <td>${rectangle.Y}</td>
            <td>${rectangle.Width}</td>
            <td>${rectangle.Height}</td>
            <td>${cellContent}</td>
          
        `;

        rectangleDataBody.appendChild(row);
    }


}

let selectedRectangleIndex = null;
function displayEditForm(rectangle) {
    selectedRectangleIndex = rectangles.indexOf(rectangle);

    document.getElementById("regex").value = rectangle.regex || "";
    document.getElementById("whiteList").value = rectangle.whiteList || "";
    document.getElementById("isDuplicated").checked = rectangle.isDuplicated || false;
    document.getElementById("zoneFieldType").value = rectangle.type || "1";

    if (rectangle.IsAnchorPoint) {
        document.getElementById("regex").disabled = true;
        document.getElementById("whiteList").disabled = true;
        document.getElementById("isDuplicated").disabled = true;
        document.getElementById("zoneFieldType").disabled = true;
    } else {
        document.getElementById("regex").disabled = false;
        document.getElementById("whiteList").disabled = false;
        document.getElementById("isDuplicated").disabled = false;
        document.getElementById("zoneFieldType").disabled = false;
    }
}



canvas.on("object:modified", (e) => {
    const modifiedObject = e.target;
    if (modifiedObject) {
        const updatedRectangles = canvas.getObjects().map((obj) => {
            if (obj.type === "rect") {
                return {
                    X: obj.left,
                    Y: obj.top,
                    Width: obj.width,
                    Height: obj.height,
                    Width: obj.getScaledWidth(),
                    Height: obj.getScaledHeight(),
                    FieldId: obj.fieldId,
                    IsAnchorPoint: obj.IsAnchorPoint || false,

                };
            }
        });

        rectangles.length = 0;
        Array.prototype.push.apply(rectangles, updatedRectangles);
        displayRectangleData();
    }
});

let isDrawing = false;
let startX, startY;
let currentRect;


canvas.on("mouse:down", async (options) => {
    if (options.target && options.target !== currentRect) {
        canvas.setActiveObject(options.target);
    } else if (options.e.ctrlKey) {
        canvas.discardActiveObject();
        console.log(fieldsFromModel);
        if (fieldsFromModel) {
            const fieldOptions = {};
            fieldsFromModel.forEach((field) => {
                fieldOptions[field.id] = field.name;
            });
        
            const { isConfirmed, value } = await Swal.fire({
                title: 'Select a Field or Set Anchor Point',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Select Field',
                showDenyButton: true,
                denyButtonText: 'Set Anchor Point',
                input: 'select',
                inputOptions: fieldOptions,
                inputPlaceholder: 'Select a field',
                inputValidator: (value) => {
                    if (!value) {
                        return 'You must select a field or set an anchor point';
                    }
                }
            });

            if (isConfirmed) {

                if (value) {
                    const table = document.getElementById("rectangleData");
                    let isDuplicatedInTable = false;

                    if (table.rows.length > 1) {
                        for (let i = 1; i < table.rows.length; i++) {
                            console.log(table.rows.length);
                            console.log(table.rows[i].cells[4].innerText);

                            if (table.rows[i].cells[4].innerText === value) {
                                isDuplicatedInTable = true;
                                break;
                            }
                        }
                    } else {
                        isDuplicatedInTable = false;
                    }


                    if (!isDuplicatedInTable) {
                        const isDuplicatedInArray = rectangles.some(rectangle => rectangle.fieldId === value);
                        if (!isDuplicatedInArray) {
                            isDrawing = true;
                            startX = options.e.clientX - canvas.getElement().getBoundingClientRect().left;
                            startY = options.e.clientY - canvas.getElement().getBoundingClientRect().top;
                            currentRect = new fabric.Rect({
                                left: startX,
                                top: startY,
                                width: 1,
                                height: 1,
                                fill: "transparent",
                                stroke: "blue",
                                strokeWidth: 2,
                                borderColor: "blue",
                                cornerColor: "blue",
                                cornerSize: 10,
                                selectable: true,
                                fieldId: value,
                                isAnchorPoint: false,
                            });
                            canvas.add(currentRect);
                        } else {
                            Swal.fire('Field Already Used', 'The selected field has already been used.', 'error');
                        }
                    } else {
                        Swal.fire('Field Already in Table', 'The selected field is already in the table.', 'error');
                    }
                }
            } else if (isConfirmed === false) {


                isDrawing = true;
                startX = options.e.clientX - canvas.getElement().getBoundingClientRect().left;
                startY = options.e.clientY - canvas.getElement().getBoundingClientRect().top;
                currentRect = new fabric.Rect({
                    left: startX,
                    top: startY,
                    width: 10,
                    height: 10,
                    fill: "transparent",
                    stroke: "red",
                    strokeWidth: 2,
                    borderColor: "red",
                    cornerColor: "red",
                    cornerSize: 10,
                    selectable: true,
                    isAnchorPoint: true,
                });
                canvas.add(currentRect);
            }
        }
    }
});


canvas.on("mouse:move", (options) => {
    if (isDrawing) {
        const x = options.e.clientX - canvas.getElement().getBoundingClientRect().left;
        const y = options.e.clientY - canvas.getElement().getBoundingClientRect().top;
        const width = x - startX;
        const height = y - startY;
        currentRect.set({ width: width, height: height });
        canvas.renderAll();
    }
});

canvas.on("mouse:up", () => {
    isDrawing = false;

    if (currentRect) {
        if (currentRect.width >= 10 && currentRect.height >= 10) {
            rectangles.push({
                X: currentRect.left,
                Y: currentRect.top,
                Width: currentRect.width,
                Height: currentRect.height,
                FieldId: currentRect.fieldId,
                IsAnchorPoint: currentRect.isAnchorPoint || false,
            });

            displayRectangleData();
        } else {
            canvas.remove(currentRect);
        }

        currentRect = null;
    }
});
let zoomLevel = 1;
let isAltKeyPressed = false;

window.addEventListener("keydown", (e) => {
    if (e.key === "Alt") {
        isAltKeyPressed = true;
    }
});

window.addEventListener("keyup", (e) => {
    if (e.key === "Alt") {
        isAltKeyPressed = false;
    }
});

canvas.on("mouse:wheel", (opt) => {
    if (isAltKeyPressed) {
        const delta = opt.e.deltaY;
        let zoom = canvas.getZoom();
        zoom = zoom - delta / 200;
        zoom = Math.max(0.5, Math.min(2, zoom));

        canvas.zoomToPoint({ x: opt.e.offsetX, y: opt.e.offsetY }, zoom);

        zoomLevel = zoom;
        opt.e.preventDefault();
    }
});


let isPanning = false;
let lastPosX, lastPosY;
let isCtrlKeyHeld = false;

canvas.on("mouse:down", (options) => {
    if (!options.target && !options.e.ctrlKey) {
        isPanning = true;
        lastPosX = options.e.clientX;
        lastPosY = options.e.clientY;
    }
});

canvas.on("mouse:up", (options) => {
    if (isPanning) {
        isPanning = false;
    }
});

canvas.on("mouse:move", (options) => {
    if (isPanning) {
        const deltaX = options.e.clientX - lastPosX;
        const deltaY = options.e.clientY - lastPosY;
        lastPosX = options.e.clientX;
        lastPosY = options.e.clientY;

        canvas.relativePan(new fabric.Point(deltaX, deltaY));
    }
});

window.addEventListener("keydown", function (e) {
    if (e.key === "Delete") {
        const activeObject = canvas.getActiveObject();
        if (activeObject) {
            const confirmation = confirm("Are you sure you want to delete this object?");
            if (confirmation) {
                canvas.remove(activeObject);

                const index = rectangles.findIndex(rectangle => (
                    rectangle.X === activeObject.left / zoomLevel,
                    rectangle.Y === activeObject.top / zoomLevel,
                    rectangle.Width === activeObject.getScaledWidth() / zoomLevel,
                    rectangle.Height === activeObject.getScaledHeight() / zoomLevel
                ));
                if (index !== -1) {
                    rectangles.splice(index, 1);
                }

                const table = document.getElementById("rectangleData");
                const rows = table.getElementsByTagName("tr");
                for (let i = 0; i < rows.length; i++) {
                    const row = rows[i];
                    if (row.cells[0].innerText === (activeObject.left / zoomLevel).toFixed(2) &&
                        row.cells[1].innerText === (activeObject.top / zoomLevel).toFixed(2) &&
                        row.cells[2].innerText === (activeObject.getScaledWidth() / zoomLevel).toFixed(2) &&
                        row.cells[3].innerText === (activeObject.getScaledHeight() / zoomLevel).toFixed(2)) {
                        table.deleteRow(i);
                        break;
                    }
                }
                rectangles.length = 0;
                Array.prototype.push.apply(rectangles, updatedRectangles);
                displayRectangleData();
            }
        }
    }
});
function updatedRectangles() {
    const updatedRectangles = canvas.getObjects().map((obj) => {
        if (obj.type === "rect") {
            console.log("update");
            console.log(obj);
            return {
                X: obj.left,
                Y: obj.top,
                Width: obj.width,
                Height: obj.height,
                FieldId: obj.fieldId,
                IsAnchorPoint: obj.isAnchorPoint || false,
            };
        }
    }).filter(rectangle => !!rectangle);

    return updatedRectangles;
}


