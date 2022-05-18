import { ComponentMeta, ComponentStory } from "@storybook/react";
import { Item } from "./Item";

const loremIpsum =
  "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
  "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

export default {
  title: "Content/Home/Search/Item/Item",
  component: Item,
} as ComponentMeta<typeof Item>;

const Template: ComponentStory<typeof Item> = (args) => <Item {...args} />;

export const Default = Template.bind({});
Default.args = {
  id: "ITEM",
  img: "static/media/src/assets/icons/modules/library.svg",
  color: "#fef445",
  name: "Example item",
  description: loremIpsum,
};

export const Selected = Template.bind({});
Selected.args = {
  ...Default.args,
  isSelected: true,
};

export const WithLongDescription = Template.bind({});
WithLongDescription.args = {
  ...Default.args,
  description: `${loremIpsum}${loremIpsum}`,
};
