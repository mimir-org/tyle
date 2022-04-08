import { ComponentStory } from "@storybook/react";
import { Heading } from "./Heading";

export default {
  title: "Text/Heading",
  component: Heading,
  args: {
    children: "A pretty lengthy text displaying the capabilities of this component.",
  },
};

const Template: ComponentStory<typeof Heading> = (args) => <Heading {...args}></Heading>;

export const H1 = Template.bind({});
H1.args = {
  as: "h1",
};

export const H2 = Template.bind({});
H2.args = {
  as: "h2",
};

export const H3 = Template.bind({});
H3.args = {
  as: "h3",
};

export const H4 = Template.bind({});
H4.args = {
  as: "h4",
};

export const H5 = Template.bind({});
H5.args = {
  as: "h5",
};

export const WithEllipsis = Template.bind({});
WithEllipsis.args = {
  ...H1.args,
  useEllipsis: true,
  ellipsisMaxLines: 2,
};
WithEllipsis.decorators = [
  (Story) => (
    <div>
      <p>Container width: 400px</p>
      <br />
      <div
        style={{
          width: "400px",
          border: "2px solid red",
        }}
      >
        <Story />
      </div>
    </div>
  ),
];
