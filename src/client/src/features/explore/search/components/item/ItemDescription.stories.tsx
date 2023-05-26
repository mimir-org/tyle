import { Meta, StoryFn } from "@storybook/react";
import { ItemDescription } from "features/explore/search/components/item/ItemDescription";

export default {
  title: "Features/Explore/Search/Item/ItemDescription",
  component: ItemDescription,
} as Meta<typeof ItemDescription>;

const Template: StoryFn<typeof ItemDescription> = (args) => <ItemDescription {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Item name",
  description: "Item description that might be quite long.",
};
