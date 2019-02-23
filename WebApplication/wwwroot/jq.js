(function () {
    'use strict';
    //////////////////////////////////////////
    var lowercase = function (string) {
        return isString(string) ? string.toLowerCase() : string;
    };
    var trim = function (value) {
        return isString(value) ? value.trim() : value;
    };
    //////////////////////////////////////////
    var BOOLEAN_ATTR = {};
    forEach('multiple,selected,checked,disabled,readOnly,required,open'.split(','), function (value) {
        BOOLEAN_ATTR[lowercase(value)] = value;
    });
    //////////////////////////////////////////
    var NODE_TYPE_ELEMENT = 1;
    var NODE_TYPE_ATTRIBUTE = 2;
    var NODE_TYPE_TEXT = 3;
    var NODE_TYPE_COMMENT = 8;
    var NODE_TYPE_DOCUMENT = 9;
    var NODE_TYPE_DOCUMENT_FRAGMENT = 11;
    var hasOwnProperty = Object.prototype.hasOwnProperty;
    //////////////////////////////////////////
    /*
 after:after,
attr:attr,
css:css,
forEach:forEach,
html:html,
isArray:isArray,
isBlankObject:isBlankObject,
isDefined:isDefined,
isFunction:isFunction,
isRegExp:isRegExp,
isString:isString,
isUndefined:isUndefined,
addClass:jqLiteAddClass,
hasClass:jqLiteHasClass,
removeClass:jqLiteRemoveClass,
nodeName_:nodeName_,
on:on,
onClick:onClick,
prepend:prepend,
removeAttr:removeAttr,
toggleClass:toggleClass,
val:val,
*/
    function after(element, newElement) {
        var parent = element.parentNode;
        if (parent) {
            parent.insertBefore(node, element.nextSibling);
        }
    }
    function attr(element, name, value) {
        var ret;
        var nodeType = element.nodeType;
        if (nodeType === NODE_TYPE_TEXT || nodeType === NODE_TYPE_ATTRIBUTE || nodeType === NODE_TYPE_COMMENT ||
            !element.getAttribute) {
            return;
        }
        var lowercasedName = lowercase(name);
        var isBooleanAttr = BOOLEAN_ATTR[lowercasedName];
        if (isDefined(value)) {
            // setter
            if (value === null || (value === false && isBooleanAttr)) {
                element.removeAttribute(name);
            } else {
                element.setAttribute(name, isBooleanAttr ? lowercasedName : value);
            }
        } else {
            // getter
            ret = element.getAttribute(name);
            if (isBooleanAttr && ret !== null) {
                ret = lowercasedName;
            }
            // Normalize non-existing attributes to undefined (as jQuery).
            return ret === null ? undefined : ret;
        }
    }
    function css(element, name, value) {
        name = cssKebabToCamel(name);
        if (isDefined(value)) {
            element.style[name] = value;
        } else {
            return element.style[name];
        }
    }
    function forEach(obj, iterator, context) {
        var key, length;
        if (obj) {
            if (isFunction(obj)) {
                for (key in obj) {
                    if (key !== 'prototype' && key !== 'length' && key !== 'name' && obj.hasOwnProperty(key)) {
                        iterator.call(context, obj[key], key, obj);
                    }
                }
            } else if (isArray(obj) || isArrayLike(obj)) {
                var isPrimitive = typeof obj !== 'object';
                for (key = 0, length = obj.length; key < length; key++) {
                    if (isPrimitive || key in obj) {
                        iterator.call(context, obj[key], key, obj);
                    }
                }
            } else if (obj.forEach && obj.forEach !== forEach) {
                obj.forEach(iterator, context, obj);
            } else if (isBlankObject(obj)) {
                // createMap() fast path --- Safe to avoid hasOwnProperty check because prototype chain is empty
                for (key in obj) {
                    iterator.call(context, obj[key], key, obj);
                }
            } else if (typeof obj.hasOwnProperty === 'function') {
                // Slow path for objects inheriting Object.prototype, hasOwnProperty check needed
                for (key in obj) {
                    if (obj.hasOwnProperty(key)) {
                        iterator.call(context, obj[key], key, obj);
                    }
                }
            } else {
                // Slow path for objects which do not have a method `hasOwnProperty`
                for (key in obj) {
                    if (hasOwnProperty.call(obj, key)) {
                        iterator.call(context, obj[key], key, obj);
                    }
                }
            }
        }
        return obj;
    }
    function html(element, value) {
        if (isUndefined(value)) {
            return element.innerHTML;
        }
        element.innerHTML = value;
    }
    function isArray(arr) {
        return Array.isArray(arr) || arr instanceof Array;
    }
    function isBlankObject(value) {
        return value !== null && typeof value === 'object' && !getPrototypeOf(value);
    }
    function isDefined(value) {
        return typeof value !== 'undefined';
    }
    function isFunction(value) {
        return typeof value === 'function';
    }
    function isRegExp(value) {
        return toString.call(value) === '[object RegExp]';
    }
    function isString(value) {
        return typeof value === 'string';
    }
    function isUndefined(value) {
        return typeof value === 'undefined';
    }
    function jqLiteAddClass(element, cssClasses) {
        if (cssClasses && element.setAttribute) {
            var existingClasses = (' ' + (element.getAttribute('class') || '') + ' ')
                .replace(/[\n\t]/g, ' ');
            var newClasses = existingClasses;
            forEach(cssClasses.split(' '), function (cssClass) {
                cssClass = trim(cssClass);
                if (newClasses.indexOf(' ' + cssClass + ' ') === -1) {
                    newClasses += cssClass + ' ';
                }
            });
            if (newClasses !== existingClasses) {
                element.setAttribute('class', trim(newClasses));
            }
        }
    }
    function jqLiteHasClass(element, selector) {
        if (!element.getAttribute) return false;
        return ((' ' + (element.getAttribute('class') || '') + ' ').replace(/[\n\t]/g, ' ').indexOf(' ' + selector + ' ') > -1);
    }
    function jqLiteRemoveClass(element, cssClasses) {
        if (cssClasses && element.setAttribute) {
            var existingClasses = (' ' + (element.getAttribute('class') || '') + ' ')
                .replace(/[\n\t]/g, ' ');
            var newClasses = existingClasses;
            forEach(cssClasses.split(' '), function (cssClass) {
                cssClass = trim(cssClass);
                newClasses = newClasses.replace(' ' + cssClass + ' ', ' ');
            });
            if (newClasses !== existingClasses) {
                element.setAttribute('class', trim(newClasses));
            }
        }
    }
    function nodeName_(element) {
        return lowercase(element.nodeName || (element[0] && element[0].nodeName));
    }
    function on(element, eventType, callback) {
        element.addEventListener(eventType, callback);
    }
    function onClick(element, callback) {
        element.addEventListener('click', callback);
    }
    function prepend(element, node) {
        if (element.nodeType === NODE_TYPE_ELEMENT) {
            var index = element.firstChild;
            element.insertBefore(node, index);
        }
    }
    function removeAttr(element, name) {
        element.removeAttribute(name);
    }
    function toggleClass(element, selector, condition) {
        if (selector) {
            forEach(selector.split(' '), function (className) {
                var classCondition = condition;
                if (isUndefined(classCondition)) {
                    classCondition = !jqLiteHasClass(element, className);
                }
                (classCondition ? jqLiteAddClass : jqLiteRemoveClass)(element, className);
            });
        }
    }
    function val(element, value) {
        if (isUndefined(value)) {
            if (element.multiple && nodeName_(element) === 'select') {
                var result = [];
                forEach(element.options, function (option) {
                    if (option.selected) {
                        result.push(option.value || option.text);
                    }
                });
                return result;
            }
            return element.value;
        }
        element.value = value;
    }
    //////////////////////////////////////////
    window['jq'] = {
        after: after,
        attr: attr,
        css: css,
        forEach: forEach,
        html: html,
        isArray: isArray,
        isBlankObject: isBlankObject,
        isDefined: isDefined,
        isFunction: isFunction,
        isRegExp: isRegExp,
        isString: isString,
        isUndefined: isUndefined,
        addClass: jqLiteAddClass,
        hasClass: jqLiteHasClass,
        removeClass: jqLiteRemoveClass,
        nodeName_: nodeName_,
        on: on,
        onClick: onClick,
        prepend: prepend,
        removeAttr: removeAttr,
        toggleClass: toggleClass,
        val: val,
        //////////////////////////////////////////
        text: (function () {
            getText.$dv = '';
            return getText;
            function getText(element, value) {
                if (isUndefined(value)) {
                    var nodeType = element.nodeType;
                    return (nodeType === NODE_TYPE_ELEMENT || nodeType === NODE_TYPE_TEXT) ? element.textContent : '';
                }
                element.textContent = value;
            }
        })(),
        trim: trim,
    };
})();
