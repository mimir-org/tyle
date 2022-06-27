import styled from "styled-components/macro";
import { getTextRole, typographyMixin } from "../../../../../../complib/mixins";
import { Typography } from "../../../../../../complib/props";

export const TerminalTableContainer = styled.table`
  border-collapse: collapse;
`;

type TerminalTableHeaderProps = Pick<Typography, "textAlign">;

export const TerminalTableHeader = styled.th<TerminalTableHeaderProps>`
  padding-bottom: ${(props) => props.theme.tyle.spacing.l};

  &:not(:first-of-type) {
    padding-left: ${(props) => props.theme.tyle.spacing.l};
    padding-right: ${(props) => props.theme.tyle.spacing.l};
  }

  color: ${(props) => props.theme.tyle.color.sys.primary.base};

  vertical-align: top;
  ${getTextRole("label-large")};
  ${typographyMixin};
`;

type TerminalTableDataProps = Pick<Typography, "textAlign">;

export const TerminalTableData = styled.td<TerminalTableDataProps>`
  border-bottom: 1px solid ${(props) => props.theme.tyle.color.sys.outline.base};
  padding: ${(props) => props.theme.tyle.spacing.l} 0;

  &:not(:first-of-type) {
    padding-left: ${(props) => props.theme.tyle.spacing.l};
    padding-right: ${(props) => props.theme.tyle.spacing.l};
  }

  color: ${(props) => props.theme.tyle.color.sys.background.on};
  ${getTextRole("body-medium")};
  ${typographyMixin};
`;
