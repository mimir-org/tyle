import { ChevronDoubleLeft, ChevronDoubleRight, ChevronLeft, ChevronRight } from "@styled-icons/heroicons-outline";
import { Button } from "complib/buttons";
import { Flexbox } from "complib/layouts";
import { useSearchParams } from "react-router-dom";
import { useTheme } from "styled-components";

interface SearchNavigationProps {
  numPages: number;
}

/**
 * Returns the start and end of the pagination range.
 * @param pageNum - the current page number
 * @param numPages - the total number of pages
 * @param range - the range of pages to show on either side of the current page
 */
const getPaginationRange = (pageNum: number, numPages: number, range: number) => {
  const start = Math.max(1, pageNum - range);
  const end = Math.min(numPages, pageNum + range);
  return { start, end };
};

export const SearchNavigation = ({ numPages }: SearchNavigationProps) => {
  const theme = useTheme();
  const [searchParams, setSearchParams] = useSearchParams();
  const pageNum = Number(searchParams.get("page"));
  const { start, end } = getPaginationRange(pageNum, numPages, 2);

  return (
    <Flexbox gap={theme.tyle.spacing.s} alignItems={"center"} justifyContent={"center"}>
      <Flexbox justifyContent={"center"}>
        <Button
          variant="filled"
          icon={<ChevronDoubleLeft />}
          iconOnly
          onClick={() => setSearchParams({ page: "1" })}
          disabled={pageNum <= 1}
        >
          First
        </Button>
        <Button
          variant="filled"
          icon={<ChevronLeft />}
          iconOnly
          onClick={() => setSearchParams({ page: String(pageNum - 1) })}
          disabled={pageNum === 1}
        >
          Previous
        </Button>
      </Flexbox>
      {[...Array(numPages).keys()]
        .filter((x) => x + 1 >= start && x + 1 <= end)
        .map((x) => (
          <Button
            key={"navButton" + x}
            variant="filled"
            onClick={() => setSearchParams({ page: String(x + 1) })}
            disabled={x + 1 === pageNum}
          >
            {x + 1}
          </Button>
        ))}
      <Flexbox>
        <Button
          variant="filled"
          icon={<ChevronRight />}
          iconOnly
          onClick={() => setSearchParams({ page: String(pageNum + 1) })}
          disabled={pageNum >= numPages}
        >
          Next
        </Button>
        <Button
          variant="filled"
          icon={<ChevronDoubleRight />}
          iconOnly
          onClick={() => setSearchParams({ page: String(numPages) })}
          disabled={pageNum >= numPages}
        >
          Last
        </Button>
      </Flexbox>
    </Flexbox>
  );
};
