using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopGame.Models
{
    public class Pager
    {
        public Pager()
        { }
        public int TotalItems { get; private set; } //tổng số lượng item mà mình có
        public int CurrentPage { get; private set; } //trang hiện tại
        public int PageSize { get; private set; } //số lượng item trong 1 trang
        public int TotalPages { get; private set; } //tổng số trang
        public int StartPage { get; private set; } //trang bắt đầu 
        public int EndPage { get; private set; } //trang cuối


        public Pager(int totalItems, int page, int pageSize = 10)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int curentPage = page;
            int startPage = curentPage - 5;
            int endPage = curentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = curentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }
}
