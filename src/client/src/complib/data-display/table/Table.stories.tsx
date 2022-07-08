import { ComponentMeta, ComponentStory } from "@storybook/react";
import { TerminalButton } from "../../../content/home/components/about/components/terminal/TerminalButton";
import { Token } from "../../general";
import { Input, Select } from "../../inputs";
import { Flexbox } from "../../layouts";
import { Text } from "../../text";
import { Table, Tbody, Td, Th, Thead, Tr } from "./Table";

export default {
  title: "Data display/Table",
  component: Table,
  subcomponents: { Thead, Tbody, Td, Th, Tr },
  parameters: {
    docs: {
      description: {
        component: `The table component offers a default desktop experience at larger viewports, 
        but falls back to a card layout when the viewport's dimensions fall below a width of 1500px.
        Alongside the table component you will find wrappers for thead, tbody, tr, th and td.
        `,
      },
    },
  },
} as ComponentMeta<typeof Table>;

const Template: ComponentStory<typeof Table> = (args) => <Table {...args} />;

export const Default = Template.bind({});
Default.args = {
  borders: false,
  children: (
    <>
      <Thead>
        <Tr>
          <Th>Column A</Th>
          <Th>Column B</Th>
          <Th>Column C</Th>
          <Th>Column D</Th>
        </Tr>
      </Thead>
      <Tbody>
        {[...Array(3)].map((_, i) => (
          <Tr key={i}>
            <Td data-label="Column A">A rather lengthy value A</Td>
            <Td data-label="Column B">A bit shorter value B</Td>
            <Td data-label="Column C">Adequate value C</Td>
            <Td data-label="Column D">Small value D</Td>
          </Tr>
        ))}
      </Tbody>
    </>
  ),
};

export const WithBorders = Template.bind({});
WithBorders.args = {
  ...Default.args,
  borders: true,
};

export const WithCustomContent = Template.bind({});
WithCustomContent.args = {
  ...Default.args,
  children: (
    <>
      <Thead>
        <Tr>
          <Th>
            <Text as={"span"} color={"darkgreen"} variant={"title-medium"}>
              Column A
            </Text>
          </Th>
          <Th>
            <Text as={"span"} color={"darkgreen"} variant={"title-medium"}>
              Column B
            </Text>
          </Th>
          <Th>
            <Text as={"span"} color={"darkgreen"} variant={"title-medium"}>
              Column C
            </Text>
          </Th>
          <Th>
            <Text as={"span"} color={"darkgreen"} variant={"title-medium"}>
              Column D
            </Text>
          </Th>
        </Tr>
      </Thead>
      <Tbody>
        {[...Array(3)].map((_, i) => (
          <Tr key={i}>
            <Td data-label="Column A">
              <Flexbox alignItems={"center"} gap={"8px"}>
                <TerminalButton variant={"small"} as={"span"} color={"darkred"} direction={"Input"} />
                <Text as={"span"} variant={"body-medium"}>
                  Extraordinary terminal
                </Text>
              </Flexbox>
            </Td>
            <Td data-label="Column B">
              <Input placeholder={"Your value goes here..."} />
            </Td>
            <Td data-label="Column C">
              <Select
                options={[
                  { label: "one", value: "one" },
                  { label: "two", value: "two" },
                  { label: "three", value: "three" },
                ]}
              />
            </Td>
            <Td data-label="Column D">
              <Flexbox flexWrap={"wrap"} gap={"8px"}>
                <Token variant={"secondary"}>Value</Token>
                <Token variant={"secondary"}>Value</Token>
                <Token variant={"secondary"}>Value</Token>
              </Flexbox>
            </Td>
          </Tr>
        ))}
      </Tbody>
    </>
  ),
};
