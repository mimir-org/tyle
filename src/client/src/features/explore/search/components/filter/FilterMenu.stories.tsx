import { Meta, StoryFn } from "@storybook/react";
import { FilterMenu } from "features/explore/search/components/filter/FilterMenu";
import { FilterMenuGroup } from "features/explore/search/components/filter/FilterMenuGroup";
import { Filter } from "features/explore/search/types/filter";

export default {
  title: "Features/Explore/Search/Filter/FilterMenu",
  component: FilterMenu,
  subcomponents: { FilterMenuGroup },
} as Meta<typeof FilterMenu>;

const Template: StoryFn<typeof FilterMenu> = (args) => <FilterMenu {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Filter",
  filterGroups: [
    {
      name: "Filter Group A",
      filters: [
        { key: "size", label: "Large", value: "l" },
        { key: "size", label: "Medium", value: "m" },
        { key: "size", label: "Small", value: "s" },
      ],
    },
  ],
  toggleFilter: (_value: Filter) => alert("[STORYBOOK] FilterMenu.toggleFilter"),
};

export const WithActiveFilters = Template.bind({});
WithActiveFilters.args = {
  ...Default.args,
  activeFilters: [
    { key: "size", label: "Medium", value: "m" },
    { key: "size", label: "Small", value: "s" },
  ],
};

export const WithMultipleFilterGroups = Template.bind({});
WithMultipleFilterGroups.args = {
  ...WithActiveFilters.args,
  filterGroups: [
    {
      name: "Filter Group A",
      filters: [
        { key: "size", label: "Large", value: "l" },
        { key: "size", label: "Medium", value: "m" },
        { key: "size", label: "Small", value: "s" },
      ],
    },
    {
      name: "Filter Group B",
      filters: [
        { key: "weight", label: "Heavy", value: "heavy" },
        { key: "weight", label: "Average", value: "average" },
        { key: "weight", label: "Light", value: "light" },
      ],
    },
    {
      name: "Filter Group C",
      filters: [
        { key: "color", label: "Red", value: "r" },
        { key: "color", label: "Green", value: "g" },
        { key: "color", label: "Blue", value: "b" },
      ],
    },
  ],
};
