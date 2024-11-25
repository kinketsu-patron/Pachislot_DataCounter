using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace Pachislot_DataCounter.Models
{
    public class NumCounter : BindableBase
    {
        /// <summary>
        /// 数字と数字画像の1対1の対応付けを行うディクショナリ
        /// </summary>
        private Dictionary<uint, BitmapImage> m_NumDictionary;

        /// <summary>
        /// 符号
        /// </summary>
        private BitmapImage m_Sign;
        public BitmapImage Sign
        {
            get { return m_Sign; }
            set { SetProperty( ref m_Sign, value ); }
        }
        /// <summary>
        /// 6桁目の数値
        /// </summary>
        private BitmapImage m_SixthDigit;
        public BitmapImage SixthDigit
        {
            get { return m_SixthDigit; }
            set { SetProperty( ref m_SixthDigit, value ); }
        }
        /// <summary>
        /// 5桁目の数値
        /// </summary>
        private BitmapImage m_FifthDigit;
        public BitmapImage FifthDigit
        {
            get { return m_FifthDigit; }
            set { SetProperty( ref m_FifthDigit, value ); }
        }
        /// <summary>
        /// 4桁目の数値
        /// </summary>
        private BitmapImage m_ForthDigit;
        public BitmapImage ForthDigit
        {
            get { return m_ForthDigit; }
            set { SetProperty( ref m_ForthDigit, value ); }
        }
        /// <summary>
        /// 3桁目の数値
        /// </summary>
        private BitmapImage m_ThirdDigit;
        public BitmapImage ThirdDigit
        {
            get { return m_ThirdDigit; }
            set { SetProperty( ref m_ThirdDigit, value ); }
        }
        /// <summary>
        /// 2桁目の数値
        /// </summary>
        private BitmapImage m_SecondDigit;
        public BitmapImage SecondDigit
        {
            get { return m_SecondDigit; }
            set { SetProperty( ref m_SecondDigit, value ); }
        }
        /// <summary>
        /// 1桁目の数値
        /// </summary>
        private BitmapImage m_FirstDigit;
        public BitmapImage FirstDigit
        {
            get { return m_FirstDigit; }
            set { SetProperty( ref m_FirstDigit, value ); }
        }

        public NumCounter( )
        {
            m_NumDictionary = new Dictionary<uint, BitmapImage>
                        {
                                { 0, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(0).png" ) },
                                { 1, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(1).png" ) },
                                { 2, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(2).png" ) },
                                { 3, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(3).png" ) },
                                { 4, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(4).png" ) },
                                { 5, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(5).png" ) },
                                { 6, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(6).png" ) },
                                { 7, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(7).png" ) },
                                { 8, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(8).png" ) },
                                { 9, create_bitmap_image( "pack://application:,,,/Resource/数字/数字(9).png" ) }
                        };
            Sign = null;
            SixthDigit = null;
            FifthDigit = null;
            ForthDigit = null;
            ThirdDigit = null;
            SecondDigit = null;
            FirstDigit = m_NumDictionary[ 0 ];
        }

        /// <summary>
        /// 整数型の数値を設定すると適切に数値画像を選択する
        /// </summary>
        /// <param name="p_Number">整数</param>
        public void SetNumber( int p_Number )
        {
            uint abs_number;
            uint temp;

            abs_number = ( uint )Math.Abs( p_Number );

            if ( abs_number >= 0 && abs_number < 10 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = null;
                SecondDigit = null;
                FirstDigit = m_NumDictionary[ abs_number ];
                Sign = p_Number < 0 ? create_bitmap_image( "pack://application:,,,/Resource/数字/数字(Sign).png" ) : null;
            } else if ( abs_number >= 10 && abs_number < 100 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = null;
                SecondDigit = m_NumDictionary[ abs_number / 10 ];
                temp = abs_number % 10;
                FirstDigit = m_NumDictionary[ temp ];
                Sign = p_Number < 0 ? create_bitmap_image( "pack://application:,,,/Resource/数字/数字(Sign).png" ) : null;
            } else if ( abs_number >= 100 && abs_number < 1000 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = null;
                ThirdDigit = m_NumDictionary[ abs_number / 100 ];
                temp = abs_number % 100;
                SecondDigit = m_NumDictionary[ temp / 10 ];
                temp = abs_number % 10;
                FirstDigit = m_NumDictionary[ temp ];
                Sign = p_Number < 0 ? create_bitmap_image( "pack://application:,,,/Resource/数字/数字(Sign).png" ) : null;
            } else if ( abs_number >= 1000 && abs_number < 10000 )
            {
                SixthDigit = null;
                FifthDigit = null;
                ForthDigit = m_NumDictionary[ abs_number / 1000 ];
                temp = abs_number % 1000;
                ThirdDigit = m_NumDictionary[ temp / 100 ];
                temp = abs_number % 100;
                SecondDigit = m_NumDictionary[ temp / 10 ];
                temp = abs_number % 10;
                FirstDigit = m_NumDictionary[ temp ];
                Sign = p_Number < 0 ? create_bitmap_image( "pack://application:,,,/Resource/数字/数字(Sign).png" ) : null;
            } else if ( abs_number >= 10000 && abs_number < 100000 )
            {
                SixthDigit = null;
                FifthDigit = m_NumDictionary[ abs_number / 10000 ];
                temp = abs_number % 10000;
                ForthDigit = m_NumDictionary[ temp / 1000 ];
                temp = abs_number % 1000;
                ThirdDigit = m_NumDictionary[ temp / 100 ];
                temp = abs_number % 100;
                SecondDigit = m_NumDictionary[ temp / 10 ];
                temp = abs_number % 10;
                FirstDigit = m_NumDictionary[ temp ];
                Sign = p_Number < 0 ? create_bitmap_image( "pack://application:,,,/Resource/数字/数字(Sign).png" ) : null;
            } else if ( abs_number >= 100000 && abs_number < 1000000 )
            {
                SixthDigit = m_NumDictionary[ abs_number / 100000 ];
                temp = abs_number % 100000;
                FifthDigit = m_NumDictionary[ temp / 10000 ];
                temp = abs_number % 10000;
                ForthDigit = m_NumDictionary[ temp / 1000 ];
                temp = abs_number % 1000;
                ThirdDigit = m_NumDictionary[ temp / 100 ];
                temp = abs_number % 100;
                SecondDigit = m_NumDictionary[ temp / 10 ];
                temp = abs_number % 10;
                FirstDigit = m_NumDictionary[ temp ];
                Sign = p_Number < 0 ? create_bitmap_image( "pack://application:,,,/Resource/数字/数字(Sign).png" ) : null;
            } else
            {
                SixthDigit = m_NumDictionary[ 9 ];
                FirstDigit = m_NumDictionary[ 9 ];
                SecondDigit = m_NumDictionary[ 9 ];
                ThirdDigit = m_NumDictionary[ 9 ];
                ForthDigit = m_NumDictionary[ 9 ];
                FifthDigit = m_NumDictionary[ 9 ];
                Sign = p_Number < 0 ? create_bitmap_image( "pack://application:,,,/Resource/数字/数字(Sign).png" ) : null;
            }
        }

        /// <summary>
        /// 数字画像のパスを指定するとBitmapImageクラスのインスタンスにして返す
        /// </summary>
        /// <param name="p_FilePath">数字画像のパス</param>
        /// <returns>数字画像のBitmapImage</returns>
        private BitmapImage create_bitmap_image( string p_FilePath )
        {
            BitmapImage img = new BitmapImage( );

            try
            {
                img.BeginInit( );
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.UriSource = new Uri( p_FilePath, UriKind.Absolute );
                img.EndInit( );
                img.Freeze( );
            } catch ( Exception e )
            {
                Debug.WriteLine( e.Message );
            }

            return img;
        }
    }
}
