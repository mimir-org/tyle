import { getTextRole } from "@mimirorg/component-library";
import styled from "styled-components/macro";

export const Dl = styled.dl`
  display: grid;
  align-items: baseline;
  grid-template-columns: 0.4fr 1fr;
  row-gap: ${(props) => props.theme.tyle.spacing.base};
  column-gap: ${(props) => props.theme.tyle.spacing.xl};

  @media screen and (${(props) => props.theme.tyle.queries.phoneAndBelow}) {
    && {
      display: flex;
      flex-direction: column;
      gap: ${(props) => props.theme.tyle.spacing.xs};
    }
  }
`;

export const Dt = styled.dt`
  ${getTextRole("label-large")};

  @media screen and (${(props) => props.theme.tyle.queries.phoneAndBelow}) {
    && {
      :not(:first-of-type) {
        margin-top: ${(props) => props.theme.tyle.spacing.s};
      }
    }
  }
`;

export const Dd = styled.dd`
  ${getTextRole("body-large")};
`;
