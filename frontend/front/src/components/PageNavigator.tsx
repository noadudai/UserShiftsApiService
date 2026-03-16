const PageNavigator = ({
    itemsPerPage,
    totalItems,
    currentPage,
    onPageChange,
}: {
    itemsPerPage: number;
    totalItems: number;
    currentPage: number;
    onPageChange: (number: number) => void;
}) => {
    const numberOfPages = Math.ceil(totalItems / itemsPerPage);
    const pageNumbers = Array.from({ length: numberOfPages }, (_, i) => i + 1);

    return (
        <div>
            <ul className="flex justify-center pt-4">
                {pageNumbers.map((number) => (
                    <li key={number}>
                        <a
                            onClick={() => {
                                onPageChange(number);
                            }}
                            href="#"
                            className={
                                currentPage === number
                                    ? 'text-custom-cream bg-amber-100 flex rounded-full p-2 mr-1.5 '
                                    : 'text-custom-cream bg-gray-300 flex hover:bg-gray-200 rounded-full p-2 mr-1.5'
                            }
                        ></a>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default PageNavigator;
