import { ChevronDoubleLeft, ChevronDoubleRight, ChevronLeft, ChevronRight } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { Box } from "complib/layouts";
import { useSearchParams } from "react-router-dom";

interface SearchNavigationProps {
  numPages: number;
}

export const SearchNavigation = ({ numPages }: SearchNavigationProps) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const pageNum = Number(searchParams.get("page"));

  return (
    <Box>
      {pageNum > 1 && (
        <>
          <Button variant="filled" icon={<ChevronDoubleLeft />} iconOnly onClick={() => setSearchParams({ page: "1" })}>
            First
          </Button>
          <Button
            variant="filled"
            icon={<ChevronLeft />}
            iconOnly
            onClick={() => setSearchParams({ page: String(pageNum - 1) })}
          >
            Previous
          </Button>
        </>
      )}
      {[...Array(numPages).keys()].map((x) => (
        <Button
          key={"navButton" + x}
          variant="filled"
          onClick={() => setSearchParams({ page: String(x + 1) })}
          disabled={x + 1 === pageNum}
        >
          {x + 1}
        </Button>
      ))}
      {pageNum < numPages && (
        <>
          <Button
            variant="filled"
            icon={<ChevronRight />}
            iconOnly
            onClick={() => setSearchParams({ page: String(pageNum + 1) })}
          >
            Next
          </Button>
          <Button
            variant="filled"
            icon={<ChevronDoubleRight />}
            iconOnly
            onClick={() => setSearchParams({ page: String(numPages) })}
          >
            Last
          </Button>
        </>
      )}
    </Box>
  );
};
