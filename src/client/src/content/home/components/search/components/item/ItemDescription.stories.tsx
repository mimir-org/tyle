import { ComponentMeta, ComponentStory } from "@storybook/react";
import { ItemDescription } from "./ItemDescription";

export default {
  title: "Content/Home/Search/Item/ItemDescription",
  component: ItemDescription,
  args: {
    title: "Item title",
    description: "Item description that might be quite long.",
  },
} as ComponentMeta<typeof ItemDescription>;

const Template: ComponentStory<typeof ItemDescription> = (args) => <ItemDescription {...args} />;

export const Default = Template.bind({});
