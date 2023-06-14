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
  if (pageNum - range < 1) {
    pageNum = range + 1;
  }
  if (pageNum + range > numPages) {
    pageNum = numPages - range;
  }

  const start = pageNum - range;
  const end = pageNum + range;

  return { start, end };
};

export const SearchNavigation = ({ numPages }: SearchNavigationProps) => {
  const theme = useTheme();
  const [searchParams, setSearchParams] = useSearchParams();
  const pageNum = Number(searchParams.get("page"));
  const { start, end } = getPaginationRange(pageNum, numPages, 3);

  return (
    <Flexbox gap={theme.tyle.spacing.l} alignItems={"center"} justifyContent={"center"}>
      <Flexbox justifyContent={"center"}>
        <Button
          variant="filled"
          icon={<ChevronDoubleLeft />}
          iconOnly
          onClick={() => setSearchParams({ page: "1" })}
          disabled={pageNum === 1}
        >
          First
        </Button>
      </Flexbox>
      <Flexbox justifyContent={"center"}>
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
          <span
            key={"navButton" + x}
            onClick={() => setSearchParams({ page: String(x + 1) })}
            style={{ textDecoration: x + 1 === pageNum ? "underline" : "none", cursor: "pointer" }}
          >
            {x + 1}
          </span>
        ))}
      <Flexbox justifyContent={"center"}>
        <Button
          variant="filled"
          icon={<ChevronRight />}
          iconOnly
          onClick={() => setSearchParams({ page: String(pageNum + 1) })}
          disabled={pageNum >= numPages}
        >
          Next
        </Button>
      </Flexbox>
      <Flexbox justifyContent={"center"}>
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
