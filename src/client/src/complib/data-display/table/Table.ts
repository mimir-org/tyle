import { getTextRole, sizingMixin, typographyMixin } from "complib/mixins";
import { Sizing, Typography } from "complib/props";
import { hideVisually } from "polished";
import styled, { css } from "styled-components/macro";

type TableProps = Sizing & {
  borders?: boolean;
};

/**
 * The table component offers a default desktop experience at larger viewports,
 * but falls back to a card layout when the viewport's dimensions fall below a width of 1500px.
 *
 * Alongside the table component you will find wrappers for thead, tbody, tr, th and td.
 *
 * @param borders enables borders and increases padding in table
 * @example
 * <Table>
 *  <Thead>
 *    <Tr>
 *      <Th>Column A</Th>
 *      <Th>Column B</Th>
 *      <Th>Column C</Th>
 *      <Th>Column D</Th>
 *    </Tr>
 *  </Thead>
 *  <Tbody>
 *    <Tr>
 *      <Td data-label="Column A">A rather lengthy value A</Td>
 *      <Td data-label="Column B">A bit shorter value B</Td>
 *      <Td data-label="Column C">Adequate value C</Td>
 *      <Td data-label="Column D">Small value D</Td>
 *    </Tr>
 *  </Tbody>
 * </Table>
 */
export const Table = styled.table<TableProps>`
  width: fit-content;
  border-collapse: collapse;

  ${({ borders, ...props }) =>
    borders &&
    css`
      ${Th} {
        padding-bottom: 0;
      }

      ${Td} {
        padding-top: ${props.theme.mimirorg.spacing.xl};
        padding-bottom: ${props.theme.mimirorg.spacing.xl};
      }

      ${Tr}:not(:last-of-type) {
        border-bottom: 1px solid ${props.theme.mimirorg.color.outline.base};
      }

      @media screen and ${props.theme.mimirorg.queries.laptopAndBelow} {
        ${Td} {
          padding-top: 0;
        }

        && ${Tr} {
          border-bottom: 0;
        }
      }
    `};

  ${sizingMixin};
`;

export const Thead = styled.thead`
  @media screen and ${(props) => props.theme.mimirorg.queries.laptopAndBelow} {
    ${hideVisually};
  }
`;

export const Tbody = styled.tbody`
  @media screen and ${(props) => props.theme.mimirorg.queries.laptopAndBelow} {
    display: flex;
    flex-wrap: wrap;
    gap: ${(props) => props.theme.mimirorg.spacing.xl};
  }
`;

export const Tr = styled.tr`
  @media screen and ${(props) => props.theme.mimirorg.queries.laptopAndBelow} {
    flex: 1;
    padding: ${(props) => props.theme.mimirorg.spacing.xl};
    background-color: ${(props) => props.theme.mimirorg.color.surface.base};
    border-radius: ${(props) => props.theme.mimirorg.border.radius.large};
    width: min(250px, fit-content);
  }
`;

type ThProps = Pick<Typography, "textAlign">;

export const Th = styled.th<ThProps>`
  padding-bottom: ${(props) => props.theme.mimirorg.spacing.s};
  padding-right: ${(props) => props.theme.mimirorg.spacing.l};

  &:not(:first-of-type) {
    padding-left: ${(props) => props.theme.mimirorg.spacing.l};
  }

  color: ${(props) => props.theme.mimirorg.color.surface.variant.on};

  white-space: nowrap;
  text-align: left;
  vertical-align: top;
  ${getTextRole("label-large")};
  ${typographyMixin};
`;

type TdProps = Pick<Typography, "textAlign">;

export const Td = styled.td<TdProps>`
  padding-bottom: ${(props) => props.theme.mimirorg.spacing.l};
  padding-right: ${(props) => props.theme.mimirorg.spacing.l};

  :not(:first-of-type) {
    padding-left: ${(props) => props.theme.mimirorg.spacing.l};
  }

  color: ${(props) => props.theme.mimirorg.color.background.on};

  text-align: left;
  vertical-align: top;
  ${getTextRole("body-medium")};
  ${typographyMixin};

  @media screen and ${(props) => props.theme.mimirorg.queries.laptopAndBelow} {
    && {
      display: flex;
      flex-direction: column;
      flex-wrap: wrap;
      justify-content: space-between;
      gap: ${(props) => props.theme.mimirorg.spacing.base};
      width: fit-content;
      padding-left: 0;
      padding-right: 0;
    }

    :last-of-type {
      padding-bottom: 0;
    }

    ::before {
      content: attr(data-label);
      color: ${(props) => props.theme.mimirorg.color.surface.on};
      ${getTextRole("title-small")};
    }
  }
`;
