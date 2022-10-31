import { TyleTheme } from "complib/core";
import { translucify } from "complib/mixins";
import { Ref } from "react";
import ReactSelect, { GroupBase, Props, StylesConfig } from "react-select";
import { default as ReactSelectType } from "react-select/base";
import { useTheme } from "styled-components";

/**
 * Select component built on top of react-select. Offers a generic api to allow for using almost any data-structure as options.
 *
 * See documentation links below for details.
 * @see https://react-select.com
 * @see https://react-select.com/typescript#select-generics
 *
 * @param selectRef reference forwarded to underlying react-select component
 * @param reactSelectProps all built-in react-select properties
 * @constructor
 */
export const Select = <Option, IsMulti extends boolean = false, Group extends GroupBase<Option> = GroupBase<Option>>({
  selectRef,
  ...reactSelectProps
}: Props<Option, IsMulti, Group> & { selectRef?: Ref<ReactSelectType<Option, IsMulti, Group>> }) => {
  const theme = useTheme();
  const customStyles = getReactSelectStyle<Option, IsMulti, Group>(theme.tyle);

  return <ReactSelect ref={selectRef} styles={customStyles} {...reactSelectProps} />;
};

/**
 * Uses the css-in-js wrapper for styling react-select
 *
 * See documentation link below for details.
 * @see https://react-select.com/styles#styles
 *
 * @param theme used to style the third party component to match the application's design
 */
const getReactSelectStyle = <Option, IsMulti extends boolean, Group extends GroupBase<Option>>(
  theme: TyleTheme
): StylesConfig<Option, IsMulti, Group> => ({
  container: (base, state) => ({
    ...base,
    height: state.isMulti ? "auto" : "40px",
  }),
  control: (base, state) => ({
    ...base,
    boxShadow: "none",
    width: "250px",
    minHeight: "40px",
    borderWidth: "1px",
    borderStyle: "solid",
    borderColor: theme.color.sys.outline.base,
    backgroundColor: state.isDisabled ? translucify(theme.color.sys.surface.on, 0.08) : theme.color.sys.pure.base,
    outline: state.isFocused ? `1px solid ${theme.color.sys.primary.base}` : "revert",
    outlineOffset: "1px",
    "&:hover": {},
    textOverflow: "ellipsis",
    overflow: "hidden",
    whiteSpace: "nowrap",
  }),
  placeholder: (base) => ({
    ...base,
    color: theme.color.sys.outline.base,
  }),
  menu: (base) => ({
    ...base,
    width: "250px",
    color: theme.color.sys.surface.on,
    boxShadow: "none",
  }),
  menuList: (base) => ({
    ...base,
    boxShadow: "none",
    borderWidth: "1px",
    borderStyle: "solid",
    borderColor: theme.color.sys.outline.base,
    borderRadius: theme.border.radius.medium,
  }),
  valueContainer: (base) => ({
    ...base,
    paddingLeft: theme.spacing.l,
    paddingRight: theme.spacing.l,
    paddingTop: theme.spacing.xs,
    paddingBottom: theme.spacing.xs,
  }),
  singleValue: (base) => ({
    ...base,
    margin: 0,
    color: theme.color.sys.background.on,
    font: theme.typography.sys.roles.body.large.font,
    letterSpacing: theme.typography.sys.roles.body.large.letterSpacing,
    lineHeight: theme.typography.sys.roles.body.large.lineHeight,
  }),
  multiValue: (base) => ({
    ...base,
    color: theme.color.sys.secondary.on,
    backgroundColor: theme.color.sys.secondary.container?.base,
    borderRadius: theme.border.radius.small,
    font: theme.typography.sys.roles.label.large.font,
    letterSpacing: theme.typography.sys.roles.label.large.letterSpacing,
    lineHeight: theme.typography.sys.roles.label.large.lineHeight,
  }),
  multiValueLabel: (base) => ({
    ...base,
    padding: theme.spacing.s,
    paddingLeft: theme.spacing.base,
  }),
  multiValueRemove: (base) => ({
    ...base,
    paddingLeft: theme.spacing.s,
    paddingRight: theme.spacing.s,
  }),
  option: (base, state) => {
    let backgroundColor = theme.color.sys.pure.base;

    if (state.isFocused) {
      backgroundColor = theme.color.sys.secondary.container?.base ?? "";
    } else if (state.isSelected) {
      backgroundColor = theme.color.sys.tertiary.container?.base ?? "";
    }

    return {
      ...base,
      backgroundColor,
      paddingLeft: theme.spacing.l,
      color: theme.color.sys.background.on,
    };
  },
});
