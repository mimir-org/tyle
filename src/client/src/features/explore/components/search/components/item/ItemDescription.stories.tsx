import { ComponentMeta, ComponentStory } from "@storybook/react";
import { ItemDescription } from "./ItemDescription";

export default {
  title: "Explore/Search/Item/ItemDescription",
  component: ItemDescription,
} as ComponentMeta<typeof ItemDescription>;

const Template: ComponentStory<typeof ItemDescription> = (args) => <ItemDescription {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Item name",
  description: "Item description that might be quite long.",
};
