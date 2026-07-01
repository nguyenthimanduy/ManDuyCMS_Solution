import { useState, useEffect, useRef } from 'react';
import { Link, useLocation, useNavigate } from 'react-router-dom';
import {
    FiSearch,
    FiShoppingCart,
    FiUser,
    FiPackage,
    FiLogOut
} from 'react-icons/fi';
import { GiRose } from 'react-icons/gi';
import { useCart } from './CartContext';
import { useAuth } from './AuthContext';
import '../styles/Header.css';

export default function Header() {

    const [scrolled, setScrolled] = useState(false);
    const [mobileOpen, setMobileOpen] = useState(false);
    const [dropdownOpen, setDropdownOpen] = useState(false);
    const [searchQuery, setSearchQuery] = useState('');

    const location = useLocation();
    const navigate = useNavigate();

    // Hook lấy tổng số lượng sản phẩm trong giỏ
    const { totalItems } = useCart();

    const { customer, isLoggedIn, logout } = useAuth();

    const dropdownRef = useRef(null);

    useEffect(() => {
        const onScroll = () => setScrolled(window.scrollY > 10);
        window.addEventListener('scroll', onScroll);
        return () => window.removeEventListener('scroll', onScroll);
    }, []);

    useEffect(() => {
        setMobileOpen(false);
        setDropdownOpen(false);
    }, [location]);

    useEffect(() => {
        const click = (e) => {
            if (dropdownRef.current && !dropdownRef.current.contains(e.target)) {
                setDropdownOpen(false);
            }
        };

        document.addEventListener('mousedown', click);

        return () => document.removeEventListener('mousedown', click);
    }, []);

    const handleSearch = (e) => {
        e.preventDefault();

        if (!searchQuery.trim()) return;

        navigate(`/products?search=${encodeURIComponent(searchQuery.trim())}`);

        setSearchQuery('');
    };

    const isActive = (path) => location.pathname === path ? 'active' : '';

    return (
        <>
            <header className={`header ${scrolled ? 'scrolled' : ''}`}>

                <div className="container header-inner">

                    <Link to="/" className="header-logo">
                        <div className="header-logo-icon">
                            <GiRose />
                        </div>

                        Nàng<span>Quýt</span>Cosmetics
                    </Link>

                    <nav className="header-nav">
                        <Link to="/" className={isActive('/')}>Trang chủ</Link>
                        <Link to="/products" className={isActive('/products')}>Mỹ phẩm</Link>
                        <Link to="/blog" className={isActive('/blog')}>Làm đẹp</Link>
                    </nav>

                    <form className="header-search" onSubmit={handleSearch}>

                        <FiSearch className="header-search-icon" />

                        <input
                            type="text"
                            placeholder="Tìm son, serum, kem dưỡng..."
                            value={searchQuery}
                            onChange={(e) => setSearchQuery(e.target.value)}
                        />

                    </form>

                    <div className="header-actions">

                        {/* Giỏ hàng */}
                        <Link to="/cart" className="header-cart-btn">

                            <FiShoppingCart size={22} />

                            {totalItems > 0 && (
                                <span className="header-cart-badge">
                                    {totalItems > 99 ? '99+' : totalItems}
                                </span>
                            )}

                        </Link>

                        {isLoggedIn ? (

                            <div className="user-dropdown-wrapper" ref={dropdownRef}>

                                <button
                                    className="header-user-btn"
                                    onClick={() => setDropdownOpen(!dropdownOpen)}
                                >
                                    <FiUser />

                                    <span>
                                        {customer.fullName?.split(' ').pop()}
                                    </span>
                                </button>

                                <div className={`user-dropdown ${dropdownOpen ? 'open' : ''}`}>

                                    <div className="user-dropdown-header">

                                        <p>{customer.fullName}</p>

                                        <p>{customer.email}</p>

                                    </div>

                                    <Link to="/profile">
                                        <FiUser />
                                        Tài khoản
                                    </Link>

                                    <Link to="/orders">
                                        <FiPackage />
                                        Đơn hàng
                                    </Link>

                                    <button
                                        className="logout-btn"
                                        onClick={logout}
                                    >
                                        <FiLogOut />
                                        Đăng xuất
                                    </button>

                                </div>

                            </div>

                        ) : (

                            <Link
                                to="/login"
                                className="btn btn-primary btn-sm"
                            >
                                Đăng nhập
                            </Link>

                        )}

                    </div>

                </div>

            </header>

            <div className={`mobile-nav ${mobileOpen ? 'open' : ''}`}>

                <form className="mobile-search" onSubmit={handleSearch}>

                    <FiSearch />

                    <input
                        type="text"
                        placeholder="Tìm son, serum, kem dưỡng..."
                        value={searchQuery}
                        onChange={(e) => setSearchQuery(e.target.value)}
                    />

                </form>

                <Link to="/">Trang chủ</Link>

                <Link to="/products">Mỹ phẩm</Link>

                <Link to="/blog">Làm đẹp</Link>

                <Link to="/cart">
                    Giỏ hàng ({totalItems})
                </Link>

            </div>
        </>
    );
}