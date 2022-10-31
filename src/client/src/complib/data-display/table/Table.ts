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
        padding-top: ${props.theme.tyle.spacing.xl};
        padding-bottom: ${props.theme.tyle.spacing.xl};
      }

      ${Tr}:not(:last-of-type) {
        border-bottom: 1px solid ${props.theme.tyle.color.sys.outline.base};
      }

      @media screen and ${props.theme.tyle.queries.laptopAndBelow} {
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
  @media screen and ${(props) => props.theme.tyle.queries.laptopAndBelow} {
    ${hideVisually};
  }
`;

export const Tbody = styled.tbody`
  @media screen and ${(props) => props.theme.tyle.queries.laptopAndBelow} {
    display: flex;
    flex-wrap: wrap;
    gap: ${(props) => props.theme.tyle.spacing.xl};
  }
`;

export const Tr = styled.tr`
  @media screen and ${(props) => props.theme.tyle.queries.laptopAndBelow} {
    flex: 1;
    padding: ${(props) => props.theme.tyle.spacing.xl};
    background-color: ${(props) => props.theme.tyle.color.sys.surface.base};
    border-radius: ${(props) => props.theme.tyle.border.radius.large};
    width: min(250px, fit-content);
  }
`;

type ThProps = Pick<Typography, "textAlign">;

export const Th = styled.th<ThProps>`
  padding-bottom: ${(props) => props.theme.tyle.spacing.s};
  padding-right: ${(props) => props.theme.tyle.spacing.l};

  &:not(:first-of-type) {
    padding-left: ${(props) => props.theme.tyle.spacing.l};
  }

  color: ${(props) => props.theme.tyle.color.sys.surface.variant.on};

  white-space: nowrap;
  text-align: left;
  vertical-align: top;
  ${getTextRole("label-large")};
  ${typographyMixin};
`;

type TdProps = Pick<Typography, "textAlign">;

export const Td = styled.td<TdProps>`
  padding-bottom: ${(props) => props.theme.tyle.spacing.l};
  padding-right: ${(props) => props.theme.tyle.spacing.l};

  :not(:first-of-type) {
    padding-left: ${(props) => props.theme.tyle.spacing.l};
  }

  color: ${(props) => props.theme.tyle.color.sys.background.on};

  text-align: left;
  vertical-align: top;
  ${getTextRole("body-medium")};
  ${typographyMixin};

  @media screen and ${(props) => props.theme.tyle.queries.laptopAndBelow} {
    && {
      display: flex;
      flex-direction: column;
      flex-wrap: wrap;
      justify-content: space-between;
      gap: ${(props) => props.theme.tyle.spacing.base};
      width: fit-content;
      padding-left: 0;
      padding-right: 0;
    }

    :last-of-type {
      padding-bottom: 0;
    }

    ::before {
      content: attr(data-label);
      color: ${(props) => props.theme.tyle.color.sys.surface.on};
      ${getTextRole("title-small")};
    }
  }
`;
