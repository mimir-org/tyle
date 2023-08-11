import { Header, Trigger } from "@radix-ui/react-accordion";
import { ChevronDown } from "@styled-icons/heroicons-outline";
import styled from "styled-components/macro";

export const StyledAccordionHeader = styled(Header)`
  all: unset;
  display: flex;
`;

export const StyledAccordionChevron = styled(ChevronDown)`
  transition: transform 300ms cubic-bezier(0.87, 0, 0.13, 1);

  [data-state="open"] & {
    transform: rotate(180deg);
  }

  [data-state="closed"] & {
    transform: rotate(0deg);
  }

  path {
    stroke-width: 1.5;
  }
`;

export const StyledAccordionTrigger = styled(Trigger)`
  all: unset;
  flex: 1;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: ${(props) => props.theme.mimirorg.spacing.l} ${(props) => props.theme.mimirorg.spacing.xl};

  :hover {
    text-decoration: underline;
  }
`;
