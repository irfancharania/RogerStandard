import * as React from 'react';
function Release(props) {
    var release = props.release;
    return React.createElement("div", { className: "release" },
        React.createElement("div", { className: "date" }, release.releaseDate),
        React.createElement("h2", null, release.releaseVersion),
        React.createElement("p", { className: "authors" }, release.authors.map(function (author) {
            return React.createElement("span", { className: "author", key: author },
                " ",
                author);
        })),
        React.createElement("ol", null, release.workItems.map(function (item) {
            return React.createElement("li", { key: item.id },
                React.createElement("strong", null, item.workItemString),
                item.description);
        })));
}
export default function Index(props) {
    return React.createElement("div", { className: "container" },
        React.createElement("h1", { className: "header" }, "Changelog"),
        props.releases.map(function (release) {
            return React.createElement(Release, { release: release, key: release.releaseVersion });
        }));
}
//# sourceMappingURL=Index.js.map