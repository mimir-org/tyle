import { css } from "styled-components/macro";
import { Spacing } from "../../props";

export const spacingMixin = css<Spacing>`
  padding: ${(props) => props.p};
  ${(props) =>
    props.px &&
    `
    padding-left: ${props.px};
    padding-right: ${props.px};
  `}
  ${(props) =>
    props.py &&
    `
    padding-top: ${props.py};
    padding-bottom: ${props.py};
  `}
  padding-top: ${(props) => props.pt};
  padding-right: ${(props) => props.pr};
  padding-bottom: ${(props) => props.pb};
  padding-left: ${(props) => props.pl};

  margin: ${(props) => props.m};
  ${(props) =>
    props.mx &&
    `
    margin-left: ${props.mx};
    margin-right: ${props.mx};
  `}
  ${(props) =>
    props.my &&
    `
    margin-top: ${props.my};
    margin-bottom: ${props.my};
  `}
  margin-top: ${(props) => props.mt};
  margin-right: ${(props) => props.mr};
  margin-bottom: ${(props) => props.mb};
  margin-left: ${(props) => props.ml};
`;
