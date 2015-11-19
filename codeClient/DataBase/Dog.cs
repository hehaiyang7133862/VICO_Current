using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperDog;

namespace nsVicoClient
{
    class SuperDog
    {
        private static SuperDog instance;

        private const string defaultScope = "<dogscope />";
        private const string vendorCodeString = "wBX/j8HmNCBCLUJA8bppa3pgvMXA5/GCwboc3g01Q67jpyrm8BPicMfMF1XnjZpFSXiZEdwTl2+dT+Ebvxo8Ncd776MWThdxwvFOqmuQGnLWpG5n9PWqJ1zkO2ki9YXpcmMSa+SGxnDR4VuiH3EOqHdjpMBcJvILVsmRkUaHoiUHrOIfWOwglwGr4TrCVztZHKdA4jhDaxZWQfnfDOOu84GZp5+9M3OCq2HmhxUUazjh1rfWirusobLjc16AdohirXhdULav4Oje5pHJIgLy7DlNL0OdUzUNA7mXtE2wpXH9gi7uM55sRxcry5foMf9iTw8lQO3VhHs45l1oMmbTojjFrWqJ0+loZqY0w5e8AMH8SZk7etY+yPTQEeDNOLdkGq8hkNM7Ooff6HsYv7xZbBJ9ogd8HQwurEq8g/msVZe0ADIeXo1Jy21okp2eNf7Xtb+xxxzW0dX+MaUVx6CnSxmhSJ+e/dAiN3OqoorLy45d+L86gtZNTRvarHF26LTYwegD3MR/XfVYFH9A+NRcKSR84Hac90bcnTMJw/EnQ+iDdcD/YDEh8PgdrORHF32F9n2ZBdU17X1TnRDwJoZJKvVQUjXSh9NhO+mEmC/eCzHt5LToSmKOfGhzpj+khZZ+/wyoa+Z8zox8zIBKHmmWhqz0MHNSdXYt9pzmS3QR0DEUKlBToeEh6+nIUqWNJ2gJ87CDIVj7CgyADbxVxaI+kYD2fPRIRAd/3dY7y7mITpRB9F3qxTphwwfuLU+k/TtmCYkVOMh27FFKeU/X9rt8pgO8zJXXYaXw3AnyOoROdcTvupQXWBtKKv/d/OLhrErZy6/odpxsK/Mnoqvz7DHSEIbfzFz6keAsBsBCh89WWx8QDYnlleDLbrmPWSyRS/hxluGbNkU3ZyjNMa+sNJseSyoCLWlou0p+tvrnJ05Lrnhdrew0KbTEiu3JsYx49zLDlLriC/zDEa1QVLS/Qgl6fA==";

        private SuperDog()
        { }

        public static SuperDog getInstance()
        {
            if (instance == null)
            {
                instance = new SuperDog();
            }

            return instance;
        }

        public bool checkDog()
        {
            DogStatus status;

            Dog dog = new Dog(DogFeature.Default);

            status = dog.Login(vendorCodeString, defaultScope);

            if ((null == dog) || !dog.IsLoggedIn())
                return false;

            DogFile file = dog.GetFile(1741);
            if (!file.IsLoggedIn())
            {
                return false;
            }

            int size = 0;
            status = file.FileSize(ref size);

            if (DogStatus.StatusOk != status)
            {
                return false;
            }

            // read the contents of the file into a buffer
            byte[] bytes = new byte[size];

            status = file.Read(bytes, 0, bytes.Length);

            string str = Encoding.ASCII.GetString(bytes, 0, bytes.Length);

            if (str == "VICO1741")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
