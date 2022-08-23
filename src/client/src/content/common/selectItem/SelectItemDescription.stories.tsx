import { ComponentMeta, ComponentStory } from "@storybook/react";
import { LibraryIcon } from "../../../assets/icons/modules";
import { SelectItemDescription } from "./SelectItemDescription";

export default {
  title: "Content/Common/SelectItem/SelectItemDescription",
  component: SelectItemDescription,
  args: {
    name: "Pressure, absolute",
    color: "orange",
    traits: {
      condition: "Maximum",
      qualifier: "Operating",
      source: "Calculated",
    },
  },
} as ComponentMeta<typeof SelectItemDescription>;

const Template: ComponentStory<typeof SelectItemDescription> = (args) => <SelectItemDescription {...args} />;

export const Default = Template.bind({});

export const Actionable = Template.bind({});
Actionable.args = {
  actionable: true,
  actionIcon: LibraryIcon,
  actionText: "Trigger action",
  onAction: () => alert("[STORYBOOK] AttributeDescription.Remove"),
};
