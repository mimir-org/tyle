import { ComponentStory } from "@storybook/react";
import { RadioFilters } from "features/settings/common/radio-filters/RadioFilters";

export default {
  title: "Features/Settings/Common/RadioFilters",
  component: RadioFilters,
};

const Template: ComponentStory<typeof RadioFilters> = (args) => <RadioFilters {...args} />;

export const Default = Template.bind({});
Default.args = {
  title: "Filters",
  filters: [
    { value: "A", label: "Value A" },
    { value: "B", label: "Value B" },
    { value: "C", label: "Value C" },
    { value: "D", label: "Value D" },
    { value: "E", label: "Value E" },
  ],
  onChange: () => alert("[STORYBOOK] RadioFilters.onChange"),
};

export const Controlled = Template.bind({});
Controlled.args = {
  ...Default.args,
  value: Default.args.filters?.[0].value,
};
