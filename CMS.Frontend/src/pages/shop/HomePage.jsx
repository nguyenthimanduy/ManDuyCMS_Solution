

import { useState, useEffect, useCallback } from 'react';
import { Link } from 'react-router-dom';
import { FiArrowRight, FiGrid, FiChevronLeft, FiChevronRight } from 'react-icons/fi';
import Hero from '../../components/Hero';
import ProductCard, { ProductCardSkeleton } from '../../components/ProductCard';
import {
    getProducts,
    getCategories,
    getPosts,
    getBestSellers,
    getImageUrl
} from '../../api/axiosClient';
import '../../styles/ProductList.css';

const PRODUCTS_PER_PAGE = 8;

export default function HomePage() {
    const [categories, setCategories] = useState([]);
    const [newProducts, setNewProducts] = useState([]);
    const [saleProducts, setSaleProducts] = useState([]);
    const [bestSellers, setBestSellers] = useState([]);
    const [products, setProducts] = useState([]);
    const [posts, setPosts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [currentPage, setCurrentPage] = useState(1);
    const [totalPages, setTotalPages] = useState(1);
    const [totalCount, setTotalCount] = useState(0);

    useEffect(() => {
        async function loadData() {
            try {
                const [
                    cats,
                    newest,
                    discount,
                    bestSeller,
                    blog
                ] = await Promise.all([
                    getCategories(),
                    getProducts({
                        pageSize: 4,
                        sortBy: "newest"
                    }),
                    getProducts({
                        pageSize: 4,
                        sortBy: "discount"
                    }),
                    getBestSellers(4),
                    getPosts({
                        pageSize: 3
                    })
                ]);

                setCategories(cats || []);

                setNewProducts(newest.data || []);

                setSaleProducts(discount.data || []);

                // FIX BEST SELLER
                const bestProducts =
                    Array.isArray(bestSeller)
                        ? bestSeller
                        : Array.isArray(bestSeller?.data)
                            ? bestSeller.data
                            : Array.isArray(bestSeller?.data?.items)
                                ? bestSeller.data.items
                                : Array.isArray(bestSeller?.items)
                                    ? bestSeller.items
                                    : [];

                setBestSellers(bestProducts);
                setPosts(blog.data || []);

            } catch (err) {
                console.log(err);
            }
        }

        loadData();

    }, []);


    const fetchProducts = useCallback(async (page) => {
        setLoading(true);
        const res = await getProducts({ page, pageSize: PRODUCTS_PER_PAGE });
        setProducts(res.data || []);
        setTotalPages(res.totalPages || 1);
        setTotalCount(res.totalCount || 0);
        setLoading(false);
    }, []);

    useEffect(() => { fetchProducts(currentPage) }, [currentPage, fetchProducts]);

    const Section = ({ title, sub, children, bg = "#fff" }) => (
        <section style={{ padding: "80px 0", background: bg }}>
            <div className="container">
                <div style={{ textAlign: "center", marginBottom: 40 }}>
                    <h2>{title}</h2>
                    {sub && <p>{sub}</p>}
                </div>
                {children}
            </div>
        </section>
    );

    return (
        <div>
            <Hero />

            <Section title="DANH MỤC NỔI BẬT">
                <div style={{ display: "grid", gridTemplateColumns: "repeat(auto-fit,minmax(220px,1fr))", gap: 24 }}>
                    {categories.map(c => (
                        <Link key={c.id} to={`/products?categoryId=${c.id}`} style={{ textDecoration: "none" }}>
                            <img src={getImageUrl(c.imageUrl)} alt={c.name} style={{ width: "100%", aspectRatio: "1", objectFit: "cover", borderRadius: 16 }} />
                            <h3 style={{ textAlign: "center" }}>{c.name}</h3>
                        </Link>
                    ))}
                </div>
            </Section>

            {/* NEW ARRIVAL */}
            <Section title="✨ NEW ARRIVAL" sub="Sản phẩm mới nhất">
                <div className="product-grid">
                    {newProducts.map(p => (
                        <ProductCard
                            key={p.id}
                            product={p}
                            badge="new"
                        />
                    ))}
                </div>
            </Section>


            {/* FLASH SALE */}
            <Section title="🔥 FLASH SALE" sub="Giảm giá hôm nay" bg="#fff5f8">
                <div className="product-grid">
                    {saleProducts.map(p => (
                        <ProductCard
                            key={p.id}
                            product={p}
                            badge="discount"
                        />
                    ))}
                </div>
            </Section>


            {/* BEST SELLER */}
            <Section title="⭐ BEST SELLER" sub="Sản phẩm nổi bật">
                <div className="product-grid">
                    {bestSellers.map(p => (
                        <ProductCard
                            key={p.id}
                            product={p}
                            badge="hot"
                        />
                    ))}
                </div>
            </Section>


            <Section title="✨ BLOG LÀM ĐẸP" sub="Bí quyết chăm sóc da & trang điểm">
                <div style={{ display: "grid", gridTemplateColumns: "repeat(auto-fit,minmax(320px,1fr))", gap: 24 }}>
                    {posts.map(post => (
                        <Link key={post.id} to={`/blog/${post.id}`} style={{ textDecoration: "none" }}>
                            <img src={getImageUrl(post.imageUrl)} alt={post.title} style={{ width: "100%", aspectRatio: "16/9", objectFit: "cover", borderRadius: 12 }} />
                            <h3>{post.title}</h3>
                            <p>{post.summary}</p>
                            <span>Xem thêm <FiArrowRight /></span>
                        </Link>
                    ))}
                </div>
            </Section>
        </div>);
}
