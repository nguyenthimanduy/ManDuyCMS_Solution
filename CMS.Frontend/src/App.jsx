import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Toaster } from 'react-hot-toast';
import { CartProvider } from './components/CartContext';
import { AuthProvider } from './components/AuthContext';
import Header from './components/Header';
import Footer from './components/Footer';

// Pages
import HomePage from './pages/shop/HomePage';
import ProductListPage from './pages/shop/ProductListPage';
import ProductDetailPage from './pages/product-detail/ProductDetailPage';
import CartPage from './pages/cart/CartPage';
import CheckoutPage from './pages/checkout/CheckoutPage';
import LoginPage from './pages/auth/LoginPage';
import RegisterPage from './pages/auth/RegisterPage';
import ForgotPasswordPage from './pages/user/ForgotPasswordPage';
import OrdersPage from './pages/order/OrdersPage';
import ProfilePage from './pages/user/ProfilePage';
import BlogPage from './pages/blog/BlogPage';
import BlogDetailPage from './pages/blog/BlogDetailPage';
import VnPayReturnPage from './pages/checkout/VnPayReturnPage';

import './App.css';

function App() {
    return (
        <Router>
            <AuthProvider>
                <CartProvider>
                    <div className="App">
                        <Header />

                        <main>
                            <Routes>
                                <Route path="/" element={<HomePage />} />
                                <Route path="/products" element={<ProductListPage />} />
                                <Route path="/products/:id" element={<ProductDetailPage />} />
                                <Route path="/cart" element={<CartPage />} />
                                <Route path="/checkout" element={<CheckoutPage />} />
                                <Route path="/login" element={<LoginPage />} />
                                <Route path="/register" element={<RegisterPage />} />
                                <Route path="/forgot-password" element={<ForgotPasswordPage />} />
                                <Route path="/orders" element={<OrdersPage />} />
                                <Route path="/profile" element={<ProfilePage />} />
                                <Route path="/blog" element={<BlogPage />} />
                                <Route path="/blog/:id" element={<BlogDetailPage />} />
                                <Route path="/vnpay-return" element={<VnPayReturnPage />} />
                            </Routes>
                        </main>

                        <Footer />

                        <Toaster position="bottom-right" />
                    </div>
                </CartProvider>
            </AuthProvider>
        </Router>
    );
}

export default App;