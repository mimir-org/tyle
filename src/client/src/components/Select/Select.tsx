import { Theme } from "components/TyleThemeProvider/theme";
import { Ref } from "react";
import ReactSelect, { GroupBase, Props, StylesConfig } from "react-select";
import { default as ReactSelectType } from "react-select/base";
import { useTheme } from "styled-components";

export type SelectVariant = "standard" | "compact" | "hideDisabledOptions";

interface SelectProps<Option, IsMulti extends boolean = false, Group extends GroupBase<Option> = GroupBase<Option>>
  extends Props<Option, IsMulti, Group> {
  variant?: SelectVariant;
  selectRef?: Ref<ReactSelectType<Option, IsMulti, Group>>;
}

/**
 * Select component built on top of react-select. Offers a generic api to allow for using almost any data-structure as options.
 *
 * See documentation links below for details.
 * @see https://react-select.com
 * @see https://react-select.com/typescript#select-generics
 *
 * @constructor
 * @param props takes all react-select props in addition to variant (styling) and selectRef (reference by prop)
 */
const Select = <Option, IsMulti extends boolean = false, Group extends GroupBase<Option> = GroupBase<Option>>(
  props: SelectProps<Option, IsMulti, Group>,
) => {
  const { variant, selectRef, ...reactSelectProps } = props;
  const theme = useTheme();
  const customStyles = getSelectStyle<Option, IsMulti, Group>(theme.tyle, variant);

  return <ReactSelect ref={selectRef} styles={customStyles} {...reactSelectProps} />;
};

export default Select;

/**
 * Uses the css-in-js wrapper for styling react-select
 *
 * See documentation link below for details.
 * @see https://react-select.com/styles#styles
 *
 * @param theme used to style the third party component to match the application's design
 * @param variant
 */
const getSelectStyle = <Option, IsMulti extends boolean, Group extends GroupBase<Option>>(
  theme: Theme,
  variant?: SelectVariant,
): StylesConfig<Option, IsMulti, Group> => {
  switch (variant) {
    case "compact": {
      return getCompactSelectStyle(theme);
    }
    case "hideDisabledOptions":
      return getHideDisabledStyle(theme);
    default:
      return getStandardSelectStyle(theme);
  }
};

const getCompactSelectStyle = <Option, IsMulti extends boolean, Group extends GroupBase<Option>>(
  theme: Theme,
): StylesConfig<Option, IsMulti, Group> => {
  const standard = getStandardSelectStyle<Option, IsMulti, Group>(theme);
  const compactHeight = "24px";

  return {
    ...standard,
    container: (base, state) => {
      const standardBase = standard.container && standard.container(base, state);

      return {
        ...base,
        ...standardBase,
        height: compactHeight,
      };
    },
    control: (base, state) => {
      const standardBase = standard.control && standard.control(base, state);

      return {
        ...base,
        ...standardBase,
        minHeight: "revert",
        height: compactHeight,
      };
    },
    input: (base, state) => {
      const standardBase = standard.input && standard.input(base, state);

      return {
        ...base,
        ...standardBase,
        minHeight: "1px",
        margin: 0,
      };
    },
    valueContainer: (base, state) => {
      const standardBase = standard.valueContainer && standard.valueContainer(base, state);

      return {
        ...base,
        ...standardBase,
        height: "inherit",
        paddingTop: 0,
        paddingBottom: 0,
      };
    },
    clearIndicator: (base, state) => {
      const standardBase = standard.clearIndicator && standard.clearIndicator(base, state);

      return {
        ...base,
        ...standardBase,
        paddingTop: 0,
        paddingBottom: 0,
      };
    },
    dropdownIndicator: (base, state) => {
      const standardBase = standard.dropdownIndicator && standard.dropdownIndicator(base, state);

      return {
        ...base,
        ...standardBase,
        paddingTop: 0,
        paddingBottom: 0,
      };
    },
  };
};

export const getHideDisabledStyle = <Option, IsMulti extends boolean, Group extends GroupBase<Option>>(
  theme: Theme,
): StylesConfig<Option, IsMulti, Group> => ({
  container: (base, state) => ({
    ...base,
    height: state.isMulti ? "auto" : "40px",
  }),
  control: (base, state) => ({
    ...base,
    boxShadow: "none",
    minWidth: "250px",
    width: "100%",
    minHeight: "40px",
    borderWidth: "1px",
    borderStyle: "solid",
    borderColor: theme.color.outline.base,
    backgroundColor: state.isDisabled ? theme.color.outline.base : theme.color.pure.base,
    outline: state.isFocused ? `1px solid ${theme.color.primary.base}` : "revert",
    outlineOffset: "1px",
    "&:hover": {},
    textOverflow: "ellipsis",
    overflow: "hidden",
    whiteSpace: "nowrap",
  }),
  placeholder: (base) => ({
    ...base,
    color: theme.color.outline.base,
  }),
  menu: (base) => ({
    ...base,
    minWidth: "250px",
    width: "100%",
    color: theme.color.surface.on,
    boxShadow: "none",
  }),
  menuList: (base) => ({
    ...base,
    boxShadow: "none",
    borderWidth: "1px",
    borderStyle: "solid",
    borderColor: theme.color.outline.base,
    borderRadius: theme.border.radius.medium,
  }),
  valueContainer: (base) => ({
    ...base,
    paddingLeft: theme.spacing.l,
    paddingRight: theme.spacing.l,
    paddingTop: theme.spacing.xs,
    paddingBottom: theme.spacing.xs,
  }),
  dropdownIndicator: (base, state) => ({
    ...base,
    color: state.isDisabled ? theme.color.surface.variant.on : theme.color.outline.base,
  }),
  singleValue: (base, state) => ({
    ...base,
    margin: 0,
    color: state.isDisabled ? theme.color.surface.variant.on : theme.color.background.on,
    font: theme.typography.roles.body.large.font,
    letterSpacing: theme.typography.roles.body.large.letterSpacing,
    lineHeight: theme.typography.roles.body.large.lineHeight,
  }),
  multiValue: (base, state) => ({
    ...base,
    color: state.isDisabled ? theme.color.surface.on : theme.color.background.on,
    backgroundColor: state.isDisabled ? theme.color.surface.on : theme.color.secondary.container?.base,
    borderRadius: theme.border.radius.small,
    font: theme.typography.roles.label.large.font,
    letterSpacing: theme.typography.roles.label.large.letterSpacing,
    lineHeight: theme.typography.roles.label.large.lineHeight,
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
    let backgroundColor = state.isDisabled ? theme.color.surface.variant.on : theme.color.pure.base;

    if (state.isFocused) {
      backgroundColor = theme.color.secondary.container?.base ?? "";
    } else if (state.isSelected) {
      backgroundColor = theme.color.tertiary.container?.base ?? "";
    }

    return {
      ...base,
      backgroundColor,
      paddingLeft: theme.spacing.l,
      color: theme.color.background.on,
      display: state.isDisabled ? "none" : "block",
    };
  },
});

export const getStandardSelectStyle = <Option, IsMulti extends boolean, Group extends GroupBase<Option>>(
  theme: Theme,
): StylesConfig<Option, IsMulti, Group> => ({
  container: (base, state) => ({
    ...base,
    height: state.isMulti ? "auto" : "40px",
  }),
  control: (base, state) => ({
    ...base,
    boxShadow: "none",
    minWidth: "250px",
    width: "100%",
    minHeight: "40px",
    borderWidth: "1px",
    borderStyle: "solid",
    borderColor: theme.color.outline.base,
    backgroundColor: state.isDisabled ? theme.color.outline.base : theme.color.pure.base,
    outline: state.isFocused ? `1px solid ${theme.color.primary.base}` : "revert",
    outlineOffset: "1px",
    "&:hover": {},
    textOverflow: "ellipsis",
    overflow: "hidden",
    whiteSpace: "nowrap",
  }),
  placeholder: (base) => ({
    ...base,
    color: theme.color.outline.base,
  }),
  menu: (base) => ({
    ...base,
    minWidth: "250px",
    width: "100%",
    color: theme.color.surface.on,
    boxShadow: "none",
  }),
  menuList: (base) => ({
    ...base,
    boxShadow: "none",
    borderWidth: "1px",
    borderStyle: "solid",
    borderColor: theme.color.outline.base,
    borderRadius: theme.border.radius.medium,
  }),
  valueContainer: (base) => ({
    ...base,
    paddingLeft: theme.spacing.l,
    paddingRight: theme.spacing.l,
    paddingTop: theme.spacing.xs,
    paddingBottom: theme.spacing.xs,
  }),
  dropdownIndicator: (base, state) => ({
    ...base,
    color: state.isDisabled ? theme.color.surface.variant.on : theme.color.outline.base,
  }),
  singleValue: (base, state) => ({
    ...base,
    margin: 0,
    color: state.isDisabled ? theme.color.surface.variant.on : theme.color.background.on,
    font: theme.typography.roles.body.large.font,
    letterSpacing: theme.typography.roles.body.large.letterSpacing,
    lineHeight: theme.typography.roles.body.large.lineHeight,
  }),
  multiValue: (base, state) => ({
    ...base,
    color: state.isDisabled ? theme.color.surface.on : theme.color.background.on,
    backgroundColor: state.isDisabled ? theme.color.surface.on : theme.color.secondary.container?.base,
    borderRadius: theme.border.radius.small,
    font: theme.typography.roles.label.large.font,
    letterSpacing: theme.typography.roles.label.large.letterSpacing,
    lineHeight: theme.typography.roles.label.large.lineHeight,
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
    let backgroundColor = state.isDisabled ? theme.color.surface.variant.on : theme.color.pure.base;

    if (state.isFocused) {
      backgroundColor = theme.color.secondary.container?.base ?? "";
    } else if (state.isSelected) {
      backgroundColor = theme.color.tertiary.container?.base ?? "";
    }

    return {
      ...base,
      backgroundColor,
      paddingLeft: theme.spacing.l,
      color: theme.color.background.on,
    };
  },
});
