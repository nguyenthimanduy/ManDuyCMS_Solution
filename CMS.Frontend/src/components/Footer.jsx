import { Link } from "react-router-dom";
import {
    FiFacebook,
    FiInstagram,
    FiYoutube,
    FiPhone,
    FiMail,
    FiMapPin,
    FiHeart,
} from "react-icons/fi";
import { BsFlower1 } from "react-icons/bs";
import "../styles/Footer.css";

export default function Footer() {
    const year = new Date().getFullYear();

    return (
        <footer className="footer">
            <div className="container">

                <div className="footer-grid">

                    {/* Logo */}
                    <div className="footer-brand">
                        <div className="footer-logo">
                            <div className="footer-logo-icon">
                                <BsFlower1 />
                            </div>

                            <div className="footer-logo-text">
                                <h2>Nàng Quýt</h2>
                                <span>Cosmetics</span>
                            </div>
                        </div>

                        <p className="footer-desc">
                            Nàng Quýt Cosmetics chuyên cung cấp mỹ phẩm chính hãng,
                            skincare, makeup và các sản phẩm chăm sóc sắc đẹp chất lượng,
                            giúp bạn luôn tự tin và rạng rỡ mỗi ngày.
                        </p>

                        <div className="footer-socials">
                            <a
                                href="https://facebook.com"
                                target="_blank"
                                rel="noreferrer"
                                className="footer-social-link"
                                aria-label="Facebook"
                            >
                                <FiFacebook />
                            </a>

                            <a
                                href="https://instagram.com"
                                target="_blank"
                                rel="noreferrer"
                                className="footer-social-link"
                                aria-label="Instagram"
                            >
                                <FiInstagram />
                            </a>

                            <a
                                href="https://youtube.com"
                                target="_blank"
                                rel="noreferrer"
                                className="footer-social-link"
                                aria-label="Youtube"
                            >
                                <FiYoutube />
                            </a>
                        </div>
                    </div>

                    {/* Danh mục */}
                    <div className="footer-section">
                        <h4>Danh mục</h4>

                        <Link to="/products">Tất cả sản phẩm</Link>

                        <Link to="/products?category=skincare">
                            Skincare
                        </Link>

                        <Link to="/products?category=makeup">
                            Makeup
                        </Link>

                        <Link to="/products?category=bodycare">
                            Chăm sóc cơ thể
                        </Link>

                        <Link to="/blog">Tin tức làm đẹp</Link>
                    </div>

                    {/* Hỗ trợ */}
                    <div className="footer-section">
                        <h4>Hỗ trợ khách hàng</h4>

                        <Link to="/about">
                            Giới thiệu
                        </Link>

                        <Link to="/policy">
                            Chính sách đổi trả
                        </Link>

                        <Link to="/shipping">
                            Chính sách vận chuyển
                        </Link>

                        <Link to="/privacy">
                            Chính sách bảo mật
                        </Link>

                        <Link to="/contact">
                            Liên hệ
                        </Link>
                    </div>

                    {/* Liên hệ */}
                    <div className="footer-section">
                        <h4>Thông tin liên hệ</h4>

                        <a href="tel:085 475 3010">
                            <FiPhone />
                            <span>085 475 3010</span>
                        </a>

                        <a href="mailto:namdinh240505@gmail.com">
                            <FiMail />
                            <span>duyman187@gmail.com</span>
                        </a>

                        <a
                            href="https://maps.google.com"
                            target="_blank"
                            rel="noreferrer"
                        >
                            <FiMapPin />
                            <span>TP. Hồ Chí Minh</span>
                        </a>
                    </div>

                </div>

                <div className="footer-bottom">

                    <p>
                        © {year} <strong>Nàng Quýt Cosmetics</strong>. All rights reserved.
                    </p>

                    <p className="footer-love">
                        Made with <FiHeart /> for Beauty Lovers
                    </p>

                </div>

            </div>
        </footer>
    );
}