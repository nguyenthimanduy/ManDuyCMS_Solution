import { useState, useEffect, useCallback, useRef } from "react";
import { Link } from "react-router-dom";
import {
    FiArrowRight,
    FiChevronLeft,
    FiChevronRight,
} from "react-icons/fi";

import { getProducts, getImageUrl } from "../api/axiosClient";
import "../styles/Hero.css";

export default function Hero() {
    const [products, setProducts] = useState([]);
    const [currentSlide, setCurrentSlide] = useState(0);
    const [loading, setLoading] = useState(true);

    const timerRef = useRef(null);

    const touchStartX = useRef(0);
    const touchEndX = useRef(0);

    useEffect(() => {
        loadProducts();
    }, []);

    async function loadProducts() {
        try {
            const res = await getProducts({
                page: 1,
                pageSize: 5,
                sortBy: "newest",
            });

            if (res?.data) {
                setProducts(res.data);
            } else {
                setProducts([]);
            }
        } catch (err) {
            console.log(err);
            setProducts([]);
        } finally {
            setLoading(false);
        }
    }

    const nextSlide = useCallback(() => {
        if (products.length <= 1) return;

        setCurrentSlide((prev) => (prev + 1) % products.length);
    }, [products]);

    const prevSlide = useCallback(() => {
        if (products.length <= 1) return;

        setCurrentSlide(
            (prev) => (prev - 1 + products.length) % products.length
        );
    }, [products]);

    useEffect(() => {
        if (products.length <= 1) return;

        timerRef.current = setInterval(() => {
            setCurrentSlide((prev) => (prev + 1) % products.length);
        }, 5000);

        return () => clearInterval(timerRef.current);
    }, [products]);

    const resetTimer = () => {
        clearInterval(timerRef.current);

        timerRef.current = setInterval(() => {
            setCurrentSlide((prev) => (prev + 1) % products.length);
        }, 5000);
    };

    const handleNext = () => {
        nextSlide();
        resetTimer();
    };

    const handlePrev = () => {
        prevSlide();
        resetTimer();
    };

    const handleTouchStart = (e) => {
        touchStartX.current = e.changedTouches[0].screenX;
    };

    const handleTouchEnd = (e) => {
        touchEndX.current = e.changedTouches[0].screenX;

        if (touchStartX.current - touchEndX.current > 50) {
            handleNext();
        }

        if (touchEndX.current - touchStartX.current > 50) {
            handlePrev();
        }
    };

    useEffect(() => {
        const handleKey = (e) => {
            if (e.key === "ArrowLeft") handlePrev();
            if (e.key === "ArrowRight") handleNext();
        };

        window.addEventListener("keydown", handleKey);

        return () => window.removeEventListener("keydown", handleKey);
    }, [products]);

    if (loading) {
        return (
            <section className="hero-loading">
                <div className="hero-spinner"></div>
            </section>
        );
    }

    if (products.length === 0) {
        return null;
    }
    return (
        <section
            className="hero-fullscreen"
            onTouchStart={handleTouchStart}
            onTouchEnd={handleTouchEnd}
        >
            {products.map((product, index) => {
                const active = index === currentSlide;

                return (
                    <div
                        key={product.id}
                        className={`hero-fs-slide ${active ? "active" : ""}`}
                    >
                        <div className="hero-fs-overlay"></div>

                        <div className="hero-container">

                            {/* Left Content */}
                            <div className="hero-left">

                                <span className="hero-fs-badge">
                                    ✨ Mỹ phẩm chính hãng
                                </span>

                                <h1 className="hero-fs-title">
                                    {product.name}
                                </h1>

                                <p className="hero-fs-desc">
                                    {product.description ||
                                        "Sản phẩm chăm sóc da chính hãng dành cho phái đẹp."}
                                </p>

                                <div className="hero-price">
                                    {Number(product.price).toLocaleString("vi-VN")}đ
                                </div>

                                <div className="hero-fs-actions">
                                    <Link
                                        to={`/products/${product.id}`}
                                        className="hero-fs-btn-primary"
                                    >
                                        Mua ngay
                                        <FiArrowRight />
                                    </Link>

                                    <Link
                                        to="/products"
                                        className="hero-fs-btn-outline"
                                    >
                                        Xem tất cả
                                    </Link>
                                </div>

                            </div>

                            {/* Right Image */}
                            <div className="hero-right">

                                <img
                                    src={getImageUrl(product.imageUrl)}
                                    alt={product.name}
                                    className="hero-product-image"
                                />

                            </div>

                        </div>
                    </div>
                );
            })}

            {products.length > 1 && (
                <>
                    <button
                        className="hero-fs-arrow hero-fs-arrow-left"
                        onClick={handlePrev}
                    >
                        <FiChevronLeft />
                    </button>

                    <button
                        className="hero-fs-arrow hero-fs-arrow-right"
                        onClick={handleNext}
                    >
                        <FiChevronRight />
                    </button>

                    <div className="hero-fs-dots">
                        {products.map((_, index) => (
                            <button
                                key={index}
                                className={`hero-fs-dot ${index === currentSlide ? "active" : ""
                                    }`}
                                onClick={() => {
                                    setCurrentSlide(index);
                                    resetTimer();
                                }}
                            />
                        ))}
                    </div>

                    <div className="hero-fs-progress">
                        <div
                            key={currentSlide}
                            className="hero-fs-progress-bar"
                        ></div>
                    </div>
                </>
            )}

        </section>
    );
}