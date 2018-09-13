var petColors = {
    "RAT": "#cc33ff",
    "DOG": "#3399ff",
    "CAT": "#ff80b3",
    "HAMSTER": "#75d778",
    "PARROT": "#ff3333",
    "FISH": "#ff9933",
    "DEFAULT": "#4d79ff"
};

var drawBubbleChart = function (jsonData) {
    var data = jsonData;
    var bubbleChart = new d3.svg.BubbleChart({
        supportResponsive: true,
        size: 600,
        innerRadius: 600 / 3.5,
        radiusMin: 50,
        data: {
            items: data,
            eval: function (item) { return item.count; },
            classed: function (item) { return item.text; },
            color: function (item, i) {
                // Color of the pet or default if not found
                var color = petColors[item.type];
                if (color == undefined) {
                    color = petColors["DEFAULT"];
                }
                return color;
            }
        },
        plugins: [
            {
                name: "lines",
                options: {
                    format: [
                        {// Line #0
                            textField: "count",
                            classed: { count: true },
                            style: {
                                "font-size": "28px",
                                "font-family": "Source Sans Pro, sans-serif",
                                "text-anchor": "middle",
                                fill: "white"
                            },
                            attr: {
                                dy: "0px",
                                x: function (d) { return d.cx; },
                                y: function (d) { return d.cy; }
                            }
                        },
                        {// Line #1
                            textField: "type",
                            classed: { text: true },
                            style: {
                                "font-size": "14px",
                                "font-family": "Source Sans Pro, sans-serif",
                                "text-anchor": "middle",
                                fill: "white"
                            },
                            attr: {
                                dy: "20px",
                                x: function (d) { return d.cx; },
                                y: function (d) { return d.cy; }
                            }
                        }
                    ],
                    centralFormat: [
                        {// Line #0
                            style: { "font-size": "50px" },
                            attr: {}
                        },
                        {// Line #1
                            style: { "font-size": "30px" },
                            attr: { dy: "40px" }
                        }
                    ]
                }
            }]
    });
};

var drawBasicChart = function (jsonData) {
    var svg = d3.select(".chart")
        .append("svg")
        .attr("width", 500)
        .attr("height", 300),
        margin = { top: 20, right: 20, bottom: 30, left: 40 },
        width = +svg.attr("width") - margin.left - margin.right,
        height = +svg.attr("height") - margin.top - margin.bottom;

    var x = d3.scaleBand().rangeRound([0, width]).padding(0.1),
        y = d3.scaleLinear().rangeRound([height, 0]);

    var g = svg.append("g")
        .attr("transform", "translate(" + margin.left + "," + margin.top + ")");

    x.domain(jsonData.map(function (d) { return d.name; }));
    y.domain([0, d3.max(jsonData, function (d) { return d.count; })]);

        g.append("g")
            .attr("class", "axis axis--x")
            .attr("transform", "translate(0," + height + ")")
            .call(d3.axisBottom(x));

        g.append("g")
            .attr("class", "axis axis--y")
            .call(d3.axisLeft(y))
            .append("text")
            .attr("transform", "rotate(-90)")
            .attr("y", 6)
            .attr("dy", "0.71em")
            .attr("text-anchor", "end")
            .text("Frequency");

        g.selectAll(".bar")
            .data(jsonData)
            .enter().append("rect")
            .attr("class", "bar")
            .attr("x", function (d) { return x(d.name); })
            .attr("y", function (d) { return y(d.count); })
            .attr("width", x.bandwidth())
            .attr("height", function (d) { return height - y(d.count); });
};

var drawBasicPieChart = function (jsonData) {
    var svg = d3.select(".pie")
        .append("svg")
        .attr("width", 550)
        .attr("height", 300),
        width = +svg.attr("width"),
        height = +svg.attr("height"),
        radius = Math.min(width, height) / 2,
        g = svg.append("g").attr("transform", "translate(" + width / 2 + "," + height / 2 + ")");

    var color = d3.scaleOrdinal(["#ff80b3", "#75d778", "#ff3333", "#ff9933", "#4d79ff", "#cc33ff", "#3399ff"]);

    var pie = d3.pie()
        .sort(null)
        .value(function (d) { return d.count; });

    var path = d3.arc()
        .outerRadius(radius - 10)
        .innerRadius(0);

    var label = d3.arc()
        .outerRadius(radius - 60)
        .innerRadius(radius - 60);

    var arc = g.selectAll(".arc")
        .data(pie(jsonData))
        .enter().append("g")
        .attr("class", "arc");

    arc.append("path")
        .attr("d", path)
        .attr("class", "path")
        .attr("fill", function (d) { return color(d.data.name); });

    arc.append("text")
        .attr("transform", function (d) { return "translate(" + label.centroid(d) + ")"; })
        .attr("dy", "0.25em")
        .attr("class", "pie-lable")
        .text(function (d) {
            return d.data.name + " (" + d.data.count + ")";
        });
}