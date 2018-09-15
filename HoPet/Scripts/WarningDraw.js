function drawWarningSign() {
    var context = document.getElementById("canvasId").getContext("2d");

    var width = 55;  // Triangle Width
    var height = 45; // Triangle Height
    var padding = 10;

    // Draw a path
    context.beginPath();
    context.moveTo(padding + width / 2, padding);        // Top Corner
    context.lineTo(padding + width, height + padding); // Bottom Right
    context.lineTo(padding, height + padding);         // Bottom Left
    context.closePath();

    // Create fill gradient
    var gradient = context.createLinearGradient(0, 0, 0, height);
    gradient.addColorStop(0, "#ffc821");
    gradient.addColorStop(1, "#faf100");

    // Fill the path
    context.fillStyle = gradient;
    context.fill();

    // Add a horizon reflection with a gradient to transparent
    gradient = context.createLinearGradient(0, padding, 0, padding + height);
    gradient.addColorStop(0.5, "transparent");
    gradient.addColorStop(0.5, "#dcaa09");
    gradient.addColorStop(1, "#faf100");
    
    // Fill the path
    context.fillStyle = gradient;
    context.fill();

    // Stroke the inner outline
    context.lineWidth = 5;
    context.lineJoin = "round";
    context.strokeStyle = "#333";
    context.stroke();

    context.textAlign = "center";
    context.textBaseline = "middle";
    context.font = "bold 30px 'Times New Roman', Times, serif";
    context.fillStyle = "#333";
    context.fillText("!", padding + width / 2, padding + height / 1.5);
}