(function() {
    var namespace = {
        makeResponsiveLayout: function() {
            var h = "undefined" != typeof window && window === this
                    ? this
                    : "undefined" != typeof global ? global : this,
                k = {};

            function p() {
                n();
                h.Symbol.iterator || (h.Symbol.iterator = h.Symbol("iterator"));
                p = function() {}
            }


            function r(a) {
                if (!(a instanceof Array)) {
                    a = q(a);
                    for (var b, c = []; !(b = a.next()).done;) c.push(b.value);
                    a = c
                }
                return a
            }

            function t(a, b) {
                function c() {}

                c.prototype = b.prototype;
                a.prototype = new c;
                a.prototype.constructor = a;
                for (var d in b)
                    if (Object.defineProperties) {
                        var e = Object.getOwnPropertyDescriptor(b, d);
                        e && Object.defineProperty(a, d, e)
                    } else a[d] = b[d]
            }

            var u = "function" == typeof Object.defineProperties
                ? Object.defineProperty
                : function(a, b, c) {
                    if (c.get || c.set) throw new TypeError("ES3 does not support getters and setters.");
                    a != Array.prototype && a != Object.prototype && (a[b] = c.value)
                };

            function v(a, b) {
                if (b) {
                    for (var c = h, d = a.split("."), e = 0; e < d.length - 1; e++) {
                        var f = d[e];
                        f in c || (c[f] = {});
                        c = c[f]
                    }
                    d = d[d.length - 1];
                    e = c[d];
                    b = b(e);
                    if (b != e) {
                        a = k[a] || [];
                        for (e = 0; e < a.length; e++) b = a[e](b);
                        u(c,
                            d,
                            {
                                configurable: !0,
                                writable: !0,
                                value: b
                            })
                    }
                }
            }

            function w(a, b) {
                return Object.prototype.hasOwnProperty.call(a, b)
            }


            function ca(a, b) {
                p();
                a instanceof String && (a += "");
                var c = 0,
                    d = {
                        next: function() {
                            if (c < a.length) {
                                var e = c++;
                                return {
                                    value: b(e, a[e]),
                                    done: !1
                                }
                            }
                            d.next = function() {
                                return {
                                    done: !0,
                                    value: void 0
                                }
                            };
                            return d.next()
                        }
                    };
                d[Symbol.iterator] = function() {
                    return d
                };
                return d
            }

            v("Array.prototype.keys",
                function(a) {
                    return a
                        ? a
                        : function() {
                            return ca(this,
                                function(a) {
                                    return a
                                })
                        }
                });
            v("Object.assign",
                function(a) {
                    return a
                        ? a
                        : function(a, c) {
                            for (var d = 1; d < arguments.length; d++) {
                                var e = arguments[d];
                                if (e)
                                    for (var f in e) w(e, f) && (a[f] = e[f])
                            }
                            return a
                        }
                });

            function x() {}

            function y(a, b) {
                for (var d = [], e = 2; e < arguments.length; ++e) d[e - 2] = arguments[e];
                if (!a) throw Error(2 > arguments.length ? "invalid argument" : z.apply(x, [].concat([b], r(d))));
            }

            function da(a, b) {
                for (var d = [], e = 2; e < arguments.length; ++e) d[e - 2] = arguments[e];
                if (!a) throw Error(2 > arguments.length ? "invalid state" : z.apply(x, [].concat([b], r(d))));
            }

            function z(a) {
                for (var c = [], d = 1; d < arguments.length; ++d) c[d - 1] = arguments[d];
                var e = 0;
                return a.replace(/\{}/g,
                    function() {
                        return e < c.length ? c[e++] : "{}"
                    })
            };

            function A(a) {
                this.$el = a;
                this.w = {}
            }

            A.prototype.remove = function() {
                this.$el.off();
                this.$el.remove()
            };
            A.prototype.on = function(a, b) {
                y(!this.w[a], "listener already registered for event {}", a);
                this.w[a] = b
            };
            A.prototype.off = function(a, b) {
                null == a ? this.w = {} : null != b && this.w[a] != b || delete this.w[a]
            };
            A.prototype.trigger = function(a, b) {
                for (var c = [], d = 1; d < arguments.length; ++d) c[d - 1] = arguments[d];
                (d = this.w[a]) && d.apply(null, [].concat(r(c)))
            };
            var C = new B;

            function B() {}

            B.prototype.setInterval = function(a, b) {
                return window.setInterval(a, b)
            };
            B.prototype.clearInterval = function(a) {
                window.clearInterval(a)
            };
            B.prototype.setTimeout = function(a, b) {
                return window.setTimeout(a, b)
            };
            B.prototype.clearTimeout = function(a) {
                window.clearTimeout(a)
            };
            B.prototype.requestAnimationFrame = function(a) {
                return window.requestAnimationFrame(a)
            };
            B.prototype.cancelAnimationFrame = function(a) {
                window.cancelAnimationFrame(a)
            };

            function ea(a) {
                window.requestAnimationFrame(function() {
                    window.requestAnimationFrame(a)
                })
            };

            function D(a) {
                var b = this;
                A.call(this, a);
                this.i = this.$el.find(".navigationMenuDropdown__content");
                this.H = this.$el.find(".navigationMenuDropdown__label");
                a.on("click",
                    ".navigationMenuDropdown__label",
                    function(a) {
                        return b.ca(a)
                    });
                a.on("mouseenter",
                    ".navigationMenuDropdown__label, .navigationMenuDropdown__content",
                    function() {
                        return b.trigger(E)
                    });
                a.on("mouseleave",
                    ".navigationMenuDropdown__label, .navigationMenuDropdown__content",
                    function() {
                        return b.trigger(F)
                    })
            }

            t(D, A);
            D.prototype.show = function(a) {
                var b = this;
                this.i.toggleClass("navigationMenuDropdown__content--displayAboveAnchor", a);
                this.i.toggleClass("navigationMenuDropdown__content--displayBelowAnchor", !a);
                this.i.addClass("navigationMenuDropdown__content--show");
                this.H.toggleClass("navigationMenuDropdown__label--displayAboveAnchor", a);
                this.H.toggleClass("navigationMenuDropdown__label--displayBelowAnchor", !a);
                ea(function() {
                    b.H.addClass("navigationMenuDropdown__label--open");
                    b.i.addClass("navigationMenuDropdown__content--open")
                })
            };
            D.prototype.hide = function() {
                var a = this;
                this.H.removeClass("navigationMenuDropdown__label--open");
                this.i.removeClass("navigationMenuDropdown__content--open")
                    .on("transitionend",
                        function() {
                            a.i.hasClass("navigationMenuDropdown__content--open") ||
                                a.i.removeClass("navigationMenuDropdown__content--show")
                        })
            };
            D.prototype.ca = function(a) {
                this.H.hasClass("navigationMenuDropdown__label--noContent") || (a.preventDefault(), this.trigger(G))
            };
            D.prototype.translate = function(a, b) {
                this.i.css({
                    transform: "translateX(-50%) translateX(" + a + ") translateY(" + b + ")"
                })
            };
            var G = "TOGGLE_ON_CLICK",
                E = "TOGGLE_ON_MOUSE_ENTER",
                F = "TOGGLE_ON_MOUSE_LEAVE";

            function fa(a, b, c) {
                var d = this;
                A.call(this, a);
                this.W = c;
                this.aa = b;
                a.on("click",
                    ".navigationMenuMobile__label--hasChildren",
                    function(a) {
                        a.preventDefault();
                        I($(a.target).next(".navigationMenuMobile__content"))
                    });
                a.on("click",
                    ".navigationMenuMobile__label--goUp",
                    function(a) {
                        a.preventDefault();
                        a = $(a.target)
                            .closest(".navigationMenuMobile__content")
                            .parent()
                            .closest(".navigationMenuMobile__content");
                        I(a)
                    });
                a.on("click",
                    ".navigationMenuMobile__overlay",
                    function() {
                        return ha(d)
                    });
                a.on("click",
                    ".navigationMenuMobile__button",
                    function() {
                        ia(d, d.$el.find(".navigationMenuMobile__wrapper").children());
                        d.$el.find(".navigationMenuMobile__items").addClass("navigationMenuMobile__items--open");
                        d.$el.find(".navigationMenuMobile__overlay").addClass("navigationMenuMobile__overlay--open")
                    });
                this.aa.on("resize",
                    function() {
                        return d.trigger(ja)
                    })
            }

            t(fa, A);

            function ha(a) {
                a.$el.find(".navigationMenuMobile__items").removeClass("navigationMenuMobile__items--open");
                a.$el.find(".navigationMenuMobile__overlay").removeClass("navigationMenuMobile__overlay--open")
            }

            function ia(a, b) {
                a.$el.find(".navigationMenuMobile__label").addClass("navigationMenuMobile__label--noAnimation");
                a.$el.find(".navigationMenuMobile__content").addClass("navigationMenuMobile__content--noAnimation");
                I(b);
                a.aa[0].requestAnimationFrame(function() {
                    a.$el.find(".navigationMenuMobile__label").removeClass("navigationMenuMobile__label--noAnimation");
                    a.$el.find(".navigationMenuMobile__content")
                        .removeClass("navigationMenuMobile__content--noAnimation")
                })
            }

            function I(a) {
                a.hasClass("navigationMenuMobile__wrapper") ||
                (a
                        .siblings(".navigationMenuMobile__label")
                        .addClass("navigationMenuMobile__label--hidden"),
                    a.parent()
                        .siblings()
                        .children(".navigationMenuMobile__label")
                        .addClass("navigationMenuMobile__label--hidden")
                        .end()
                        .children(".navigationMenuMobile__content")
                        .addClass("navigationMenuMobile__content--hidden"));
                a.removeClass("navigationMenuMobile__content--hidden")
                    .children()
                    .children(".navigationMenuMobile__label")
                    .removeClass("navigationMenuMobile__label--hidden")
                    .end()
                    .children(".navigationMenuMobile__content")
                    .addClass("navigationMenuMobile__content--hidden")
            }

            var ja = "WINDOW_RESIZE";

            function J(a, b, c) {
                var d = this;
                this.D = c;
                this.view = b;
                this.R = a.R;
                this.da = null;
                this.ba = 0;
                this.view.on(G,
                    function() {
                        return d.ca()
                    });
                this.view.on(E,
                    function() {
                        d.open();
                        d.D.clearTimeout(d.ba)
                    });
                this.view.on(F,
                    function() {
                        ka(d)
                    })
            }

            function la(a, b) {
                a.da = b
            }

            J.prototype.open = function() {
                this.da && this.da(this);
                var a = this.view,
                    b = a.$el.width() / 2,
                    b = a.$el.offset().left + b,
                    a = a.i.width() / 2,
                    a = b - a - 10,
                    c = this.view,
                    b = $(window).width(),
                    d = c.$el.width() / 2,
                    d = c.$el.offset().left + d,
                    c = c.i.width() / 2;
                this.view.translate(Math.min(0, b - (d + c) - 10) - Math.min(0, a) + "px", this.R ? "-100%" : "100%");
                this.view.show(this.R)
            };
            J.prototype.close = function() {
                this.view.hide()
            };
            J.prototype.ca = function() {
                this.open()
            };

            function ka(a) {
                a.D.clearTimeout(a.ba);
                a.ba = a.D.setTimeout(function() {
                        return a.close()
                    },
                    500)
            };

            function ma(a, b, c) {
                var d = this;
                this.view = b;
                this.Y = null != a.Y ? a.Y : 1100;
                this.W = c;
                this.view.on(ja,
                    function() {
                        return na(d)
                    })
            }

            function oa(a, b) {
                a.W.filter(function(a) {
                        return a !== b
                    })
                    .forEach(function(a) {
                        return a.close()
                    })
            }

            function na(a) {
                if (Number(a.view.aa.width()) > a.Y) {
                    var b = a.view;
                    b.$el
                        .find(".navigationMenuMobile__items")
                        .removeClass("navigationMenuMobile__items--mobileEnabled");
                    b.$el.find(".navigationMenuMobile__button")
                        .removeClass("navigationMenuMobile__button--mobileEnabled");
                    b.$el.find(".navigationMenu__items").addClass("navigationMenu__items--desktopEnabled");
                    ha(a.view)
                } else {
                    a = a.view;
                    a.$el.find(".navigationMenu__items")
                        .removeClass("navigationMenu__items--desktopEnabled");
                    a.$el.find(".navigationMenuMobile__items").addClass("navigationMenuMobile__items--mobileEnabled");
                    a.$el.find(".navigationMenuMobile__button").addClass("navigationMenuMobile__button--mobileEnabled");
                }
            };

            function K(a) {
                this.type = a
            }

            K.prototype.required = function(a, b) {
                b = b[a];
                if (null == b || typeof b != this.type)
                    throw new TypeError("expected " +
                        this.type +
                        ' for property "' +
                        a +
                        '", found: ' +
                        JSON.stringify(b));
                return b
            };
            K.prototype.optional = function(a, b) {
                b = b[a];
                if (null != b && typeof b != this.type)
                    throw new TypeError("expected optional " +
                        this.type +
                        ' for property "' +
                        a +
                        '", found: ' +
                        JSON.stringify(b));
                return b
            };
            var L = new K("string"),
                M = new K("number");;

            function sa(a) {
                var b = ta;
                if (!b) throw new TypeError("Cannot adopt constructor " + b);
                if ("object" !== typeof a) throw new TypeError("Cannot adopt non-object value " + a);
                if (null === a) throw new TypeError("Cannot adopt null value");
                return Object.create(b.prototype, ua(a))
            }

            function ua(a) {
                var b = Object.create(null);
                Object.keys(a)
                    .forEach(function(c) {
                        b[c] = {
                            value: a[c],
                            writable: !0,
                            enumerable: !0,
                            configurable: !0
                        }
                    });
                return b
            };

            function va(a) {
                this.size = a.size;
                this.width = a.width;
                this.height = a.height;
                this.url = a.url
            };

            function ta(a) {
                a = void 0 === a ? {} : a;
                this.version = a.version || 0;
                this.sizes = a.sizes || {};
                this.status = a.status || void 0
            }

            ta.prototype.getVersion = function() {
                return this.version
            };
            (function() {
                var a = {
                    version: 0,
                    sizes: {
                        50: {
                            size: 50,
                            width: 50,
                            height: 50,
                            url: "https://static.canva.com/static/images/default_avatar_50.png"
                        },
                        200: {
                            size: 200,
                            width: 200,
                            height: 200,
                            url: "https://static.canva.com/static/images/default_avatar_200.png"
                        }
                    }
                };
                var b = sa(a);
                b.sizes &&
                (b.sizes = Object.keys(b.sizes)
                    .reduce(function(a, d) {
                        var e = b.sizes[d];
                        a[d] = e
                            ? new va({
                                size: M.optional("size", e) || 0,
                                width: M.optional("width", e) || 0,
                                height: M.optional("height", e) || 0,
                                url: L.required("url", e)
                            })
                            : null;
                        return a
                    },
                    {}));
                return b
            })();

            function N(a) {
                this.K = a.K;
                this.N = a.N;
                this.l = a.l;
                this.o = a.o;
                this.C = a.C;
                this.u = a.u;
                this.F = a.F
            }


            function O(a, b) {
                this.config = a;
                this.fa = null;
                this.Z = [];
                this.ready = !1;
                this.ga = b
            }

            function xa(a, b) {
                var c = Object.assign({}, b);
                a.fa && (c.design = a.fa);
                c.experience = a.config.K;
                a.config.N.forEach(function(a, b) {
                    c["Experiment:" + b] = a
                });
                a.config.o && (c.brand = a.config.o);
                null != a.config.C && (c.personal = a.config.C);
                null != a.config.u && (c.cohort = a.config.u);
                a.config.F && (c.tier = a.config.F);
                ya(c);
                return c
            }

            function ya(a) {
                try {
                    var b = window.crypto || window.msCrypto,
                        c = new Uint8Array(16);
                    b.getRandomValues(c);
                    a.insert_id = btoa(String.fromCharCode.apply(null, c))
                } catch (d) {
                }
            }

            function za(a, b) {
                var c = window.lastUserTraits || Q(new R),
                    d = S(new R, "__user", a);
                a = a !== c.get("__user");
                if (b && T(b)) {
                    for (var e = q(b), f = e.next(); !f.done; f = e.next()) {
                        var g = q(f.value),
                            f = g.next().value,
                            g = g.next().value;
                        a = a || g !== c.get(f)
                    }
                    c = q(b.keys());
                    for (e = c.next(); !e.done; e = c.next()) e = e.value, S(d, e, b.get(e))
                }
                a && (window.lastUserTraits = Q(d));
                return a
            }

            function Aa(a, b) {
                var c = window.lastUserTraits || Q(new R);
                a = S(new R, "__user", a);
                if (c && T(c))
                    for (var d = q(c
                                 .keys()),
                        e = d.next();
                        !e.done;
                        e = d.next()) e = e.value, "__user" !== e && S(a, e, c.get(e));
                if (b && T(b))
                    for (c = q(b.keys()), e = c.next(); !e.done; e = c.next()) d = e.value, S(a, d, b.get(d));
                window.lastUserTraits = Q(a)
            }

            function T(a) {
                y(a instanceof Map,
                    "Could not convert traits to an object; traits value not considered a Map instance");
                return !0
            }

            O.prototype.zoom = function(a, b, c, d, e) {
                this.track("zoom",
                {
                    type: a,
                    section: b,
                    subsection: c,
                    location: d,
                    value: e
                })
            };

            function R() {
                this.items = []
            }

            function S(a, b, c) {
                a.items.push({
                    key: b,
                    value: c
                });
                return a
            }

            function Q(a) {
                return new Map(a.items.map(function(a) {
                    return [a.key, a.value]
                }))
            };

            function U(a) {
                N.call(this, a)
            }

            t(U, N);

            function V(a, b) {
                O.call(this, a, b);
                this.G = null
            }

            t(V, O);

            function W(a) {
                N.call(this, a);
                this.ea = a.ea
            }

            t(W, N);


            function X(a, b, c) {
                O.call(this, a, c);
                this.D = b;
                this.readyState = "PENDING";
                this.ha = 3E4;
                this.v = this.X = null
            }

            t(X, O);

            function Da(a, b) {
                var c = Y(),
                    d = new X(a, C, b);
                Z(d)
                    .then(function() {
                        for (d.ready = !0; d.Z && 0 < d.Z.length;) d.Z.shift()();
                        d.Z = null
                    })
                    .catch(function() {});
                window.segmentAnalyticsLoaded ||
                (window.segmentAnalyticsLoaded = !0, Z(d)
                    .then(function() {
                        var b = {
                            path: location.pathname,
                            url: location.href
                        };
                        Y()
                            .page(b,
                            {
                                integrations: {
                                    Leanplum: !1
                                }
                            });
                        a.l || (b = window.Appcues || null) && b.anonymous();
                        b = (new UAParser).getResult();
                        Q(S(S(new R, "Most recent browser", b.browser.name + " " + b.browser.version),
                            "timezone",
                            (new Date).getTimezoneOffset() /
                            -60))
                    })
                    .catch(function() {}), c.load(a.ea));
                return d
            }

            function Y() {
                da("undefined" !== typeof window.analytics);
                return window.analytics
            }

            X.prototype.track = function(a, b) {
                var c = this;
                Z(this)
                    .then(function(d) {
                        d.track(a,
                            xa(c, b),
                            {
                                integrations: {
                                    Leanplum: !1
                                }
                            })
                    })
                    .catch(function() {})
            };
            X.prototype.identify = function(a, b, c) {
                var d = this;
                return Z(this)
                    .then(function(e) {
                        if (!za(a, b)) return d.X || P.resolve();
                        d.X = new P(function(f) {
                            Aa(a, b);
                            e.identify(a,
                                Ea(b),
                                c,
                                function() {
                                    d.X = null;
                                    f()
                                })
                        });
                        return d.X
                    })
                    .catch(function() {})
            };
            X.prototype.alias = function(a) {
                Z(this)
                    .then(function(b) {
                        return b.alias(a)
                    })
                    .catch(function() {})
            };
            X.prototype.set = function(a) {
                return Z(this)
                    .then(function(b) {
                        return new P(function(c) {
                            return b.identify(Ea(a), c)
                        })
                    })
                    .catch(function() {})
            };
            X.prototype.union = function() {};

            function Ea(a) {
                var b = {};
                if (a && T(a)) {
                    a = q(a);
                    for (var c = a.next(); !c.done; c = a.next()) {
                        var d = q(c.value),
                            c = d.next().value,
                            d = d.next().value;
                        b[c] = d
                    }
                }
                return b
            }

            function Z(a) {
                var b = Y();
                if (null !== a.v) return a.v;
                if ("SUCCESS" === a.readyState) return P.resolve(b);
                if ("FAILURE" === a.readyState) return P.reject(Error("Segment initialization failed"));
                a.v = new P(function(c, d) {
                    var e = a.D.setTimeout(function() {
                            a.readyState = "FAILURE";
                            a.v = null;
                            d(Error("Segment initialization failed"))
                        },
                        a.ha);
                    b.ready(function() {
                        "FAILURE" !== a.readyState &&
                        (a.D.clearTimeout(e), a.readyState = "SUCCESS", a.v = null, c(Y()))
                    })
                });
                return a.v
            };

            function Ha() {
                var a = $("#grid");
                a.one("layoutComplete",
                    function() {
                        a.addClass("loaded")
                    });
                a.masonry({
                    itemSelector: ".gridItem"
                })
            }

            $(".categoryLink")
                .click(function() {
                    var a = $(this).data("category");
                    $('.theme[data-category\x3d"' + a + '"]').toggle();
                    Ha()
                });
            Ha();
            (function(a) {
                a.W.forEach(function(b) {
                    la(b,
                        function() {
                            return oa(a, b)
                        })
                });
                na(a)
            })(function(a, b) {
                var c = b.W.map(function(a) {
                    return new J({
                            R: !1
                        },
                        a,
                        C)
                });
                return new ma(a, b, c)
            }({
                    Y: 900
                },
                function(a) {
                    var b = a.find(".navigationMenuDropdown")
                        .toArray()
                        .map(function(a) {
                            return new D($(a))
                        });
                    return new fa(a, $(window), b)
                }($(".navbar"))));
        }
    };

    window.responsiveManager = namespace;

})();