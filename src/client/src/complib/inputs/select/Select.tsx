import { default as ReactSelectType } from "react-select/base";
import ReactSelect, { GroupBase, Props, StylesConfig } from "react-select";
import { useTheme } from "styled-components";
import { Ref } from "react";
import { translucify } from "../../mixins";
import { TyleTheme } from "../../core";

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
  control: (base, state) => ({
    ...base,
    borderColor: theme.color.outline.base,
    outline: state.isFocused ? "1px solid black" : "revert",
    "&:hover": {
      borderColor: translucify(theme.color.primary.base, 0.5),
    },
  }),
  placeholder: (base) => ({
    ...base,
    color: theme.color.outline.base,
  }),
  menu: (base) => ({
    ...base,
    color: theme.color.surface.on,
  }),
  option: (base, state) => {
    let backgroundColor = state.isSelected ? theme.color.secondary.base : "revert";
    let color = state.isSelected ? theme.color.secondary.on : "revert";

    if (state.isFocused) {
      backgroundColor = translucify(theme.color.secondary.base, 0.5);
      color = theme.color.secondary.on;
    }

    return {
      ...base,
      backgroundColor,
      color,
      "&:hover": {
        backgroundColor: translucify(theme.color.secondary.base, 0.5),
        color: theme.color.secondary.on,
      },
    };
  },
});
